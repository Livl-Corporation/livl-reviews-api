using System.Net.Mail;
using System.Security.Claims;
using LivlReviewsApi.Attributes;
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
    [UserIdClaim]
    [Route("invite")]
    public async Task<IActionResult> Invite(InviteUserRequest request)
    {
        if (request.Email is null)
        {
            return BadRequest("Email is required");
        }
        
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null)
        {
            return Unauthorized();
        }
        var currentUser = await _userManager.FindByIdAsync(currentUserId);
        if (currentUser is null)
        {
            return Unauthorized();
        }
        
        var isCurrentUserAdmin = currentUser.Role == Role.Admin;
        if(isCurrentUserAdmin is false)
        {
            return Unauthorized();
        }
        
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is not null)
        {
            return BadRequest("User already exists");
        }
        
        var userName = new MailAddress(request.Email).User;
        var newUser = new User { Email = request.Email, Role = Role.User, UserName = userName };
        var result = await _userManager.CreateAsync(newUser);
        
        if (result.Succeeded is false)
        {
            return Problem("A problem occured while creating the user");
        }
        
        var randomToken = Guid.NewGuid().ToString();
            
        var invitationToken = new InvitationToken { Token = randomToken, InvitedById = currentUserId, InvitedUserId  = newUser.Id};
        _invitationTokenRepository.Add(invitationToken);
            
        return CreatedAtAction(nameof(Invite), new { email = request.Email, role = Role.User }, request);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if token is valid
        var invitationToken = _invitationTokenRepository.GetBy(token => 
            token.Token == request.Token).First();

        if (invitationToken is null)
        {
            return Unauthorized("Invalid token");
        }
        
        // Update existing user by adding it's password
        var user = await _userManager.FindByIdAsync(invitationToken.InvitedUserId);
        if (user is null)
        {
            return BadRequest("User not found");
        }
        
        var addPasswordResult = await _userManager.AddPasswordAsync(user, request.Password);
        if (addPasswordResult.Succeeded is false)
        {
            return Problem("A problem occured while updating the user");
        }
        
        user.EmailConfirmed = true;
        var updateEmailConfirmedResult = await _userManager.UpdateAsync(user);
        if (updateEmailConfirmedResult.Succeeded is false)
        {
            return Problem("A problem occured while updating the user");
        }
        
        _invitationTokenRepository.Delete(invitationToken);

        return Ok("User creation successfully confirmed. Good job my team!");
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
        
        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(managedUser);
        if (!isEmailConfirmed)
        {
            return Unauthorized("Email not confirmed");
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