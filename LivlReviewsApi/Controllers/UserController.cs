using System.Net.Mail;
using System.Security.Claims;
using LivlReviewsApi.Data;
using LivlReviewsApi.Enums;
using LivlReviewsApi.Repositories.Interfaces;
using LivlReviewsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviewsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IRepository<InvitationToken> _invitationTokenRepository;

    public UsersController(UserManager<User> userManager, AppDbContext context, TokenService tokenService, ILogger<UsersController> logger, IRepository<InvitationToken> invitationTokenRepository)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
        _invitationTokenRepository = invitationTokenRepository;
    }

    [Authorize]
    [HttpPost]
    [Route("invite")]
    public async Task<IActionResult> Invite(InviteUserRequest request)
    {
        if (request.Email is null)
        {
            return BadRequest("Email is required");
        }
        
        var userName = new MailAddress(request.Email).User;
        var user = new User { Email = request.Email, Role = Role.User, UserName = userName };
        var result = await _userManager.CreateAsync(user);
        
        if (result.Succeeded)
        {
            var randomToken = Guid.NewGuid().ToString();
            ClaimsPrincipal currentUser = this.User;
            var userIdClaim = currentUser.Claims.FirstOrDefault(c => c.Type == "userGUID");
            if(userIdClaim == null)
            {
                return Unauthorized();
            }
            
            var invitationToken = new InvitationToken { Token = randomToken, InvitedById = userIdClaim.Value, InvitedUserId  = user.Id};
            _invitationTokenRepository.Add(invitationToken);
            
            return CreatedAtAction(nameof(Invite), new { email = request.Email, role = Role.User }, request);
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        
        return BadRequest(ModelState);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if(request.Email is null || request.Password is null)
        {
            return BadRequest("Email and password are required");
        }
        
        string userName = new MailAddress(request.Email).User;
        
        var result = await _userManager.CreateAsync(
            new User { Email = request.Email, UserName = userName, Role = Role.User },
            request.Password!
        );

        if (result.Succeeded)
        {
            request.Password = "";
            return CreatedAtAction(nameof(Register), new { email = request.Email, role = Role.User }, request);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userManager.FindByEmailAsync(request.Email!);
        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password!);
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        
        if (userInDb is null)
        {
            return Unauthorized();
        }
        
        var accessToken = _tokenService.CreateToken(userInDb);
        await _context.SaveChangesAsync();
        
        return Ok(new LoginResponse
        {
            Email = userInDb.Email,
            Token = accessToken,
        });
    }
}