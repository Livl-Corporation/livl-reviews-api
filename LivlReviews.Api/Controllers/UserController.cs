using System.Net.Mail;
using LivlReviews.Api.Attributes;
using LivlReviews.Api.Models;
using LivlReviews.Api.Services;
using LivlReviews.Domain;
using LivlReviews.Domain.Enums;
using LivlReviews.Infra;
using LivlReviews.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LivlReviews.Api.Controllers;

[ApiController]
[Route("/[controller]")]
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

    [HttpPost]
    [Route("invite")]
    [Authorize]
    [UserIdClaim]
    public async Task<IActionResult> Invite(InviteRequest request)
    {
        if(request.Email is null) return BadRequest("Email is required");
        
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null) return Unauthorized();

        try
        {
            IInvitationSender invitationSender = new InvitationSender(
                new InvitationDelivery(_userManager, _invitationTokenRepository),
                new UserInventory(_userManager)
            );
            await invitationSender.SendInvitation(currentUserId, request.Email);
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    [HttpPost]
    [Route("confirmInvitation")]
    public async Task<IActionResult> ConfirmInvitation(ConfirmInvitationRequest request)
    {
        if (request.Token is null) return BadRequest("Token is required");
        if (request.Password is null) return BadRequest("Password is required");

        try
        {
            IInvitationConfirmator invitationConfirmator = new InvitationConfirmator(
                new InvitationTokenInventory(_invitationTokenRepository),
                new UserInventory(_userManager)
            );
            await invitationConfirmator.ConfirmUser(request.Token, request.Password);

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e);
        }
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