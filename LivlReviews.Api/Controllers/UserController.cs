using System.Net.Mail;
using LivlReviews.Api.Attributes;
using LivlReviews.Api.Models;
using LivlReviews.Api.Services;
using LivlReviews.Domain;
using LivlReviews.Domain.Administration;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Invitation;
using LivlReviews.Domain.Users;
using LivlReviews.Infra;
using LivlReviews.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using InvitationToken = LivlReviews.Infra.Data.InvitationToken;
using User = LivlReviews.Infra.Data.User;

namespace LivlReviews.Api.Controllers;

[ApiController]
[Route("/[controller]")]
public class UsersController(UserManager<User> userManager, AppDbContext context, TokenService tokenService, ILogger<UsersController> logger, IRepository<InvitationToken> invitationTokenRepository) : ControllerBase
{

    [HttpPost]
    [Route("invite")]
    [Authorize]
    [UserIdClaim]
    public async Task<IActionResult> Invite(InviteRequest request)
    {
        if(String.IsNullOrEmpty(request.Email)) return BadRequest("Email is required");
        
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null) return Unauthorized();

        try
        {
            IInvitationSender invitationSender = new InvitationSender(
                new InvitationDelivery(userManager, invitationTokenRepository),
                new UserInventory(userManager)
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
        if (String.IsNullOrEmpty(request.Token)) return BadRequest("Token is required");
        if (String.IsNullOrEmpty(request.Password)) return BadRequest("Password is required");

        try
        {
            IInvitationConfirmator invitationConfirmator = new InvitationConfirmator(
                new InvitationTokenInventory(invitationTokenRepository),
                new UserInventory(userManager)
            );
            await invitationConfirmator.ConfirmUser(request.Token, request.Password);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet()]
    [Authorize]
    [UserIdClaim]
    public async Task<IActionResult> GetUsers()
    {
        var currentUserId = HttpContext.Items["UserId"] as string;
        if (currentUserId is null) return Unauthorized();

        IInvitationTokenInventory invitationTokenInventory = new InvitationTokenInventory(invitationTokenRepository);
        IUserInventory userInventory = new UserInventory(userManager);
        IAdministrationPanel administrationPanel = new AdministrationPanel(invitationTokenInventory, userInventory);

        try
        {
            var adminUsers = await administrationPanel.GetAdminUsers(currentUserId);
            return Ok(adminUsers);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (String.IsNullOrEmpty(request.Email)) return BadRequest("Email is required");
        if (String.IsNullOrEmpty(request.Password)) return BadRequest("Password is required");
        
        string userName = new MailAddress(request.Email).User;
        
        var result = await userManager.CreateAsync(
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
        if (String.IsNullOrEmpty(request.Email)) return BadRequest("Email is required");
        if (String.IsNullOrEmpty(request.Password)) return BadRequest("Password is required");

        var managedUser = await userManager.FindByEmailAsync(request.Email!);
        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }

        if (managedUser.EmailConfirmed is false) return BadRequest("Bad credentials");

        var isPasswordValid = await userManager.CheckPasswordAsync(managedUser, request.Password!);
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        var userInDb = context.Users.FirstOrDefault(u => u.Email == request.Email);
        
        if (userInDb is null)
        {
            return Unauthorized();
        }
        
        var accessToken = tokenService.CreateToken(userInDb);
        await context.SaveChangesAsync();
        
        return Ok(new LoginResponse
        {
            Email = userInDb.Email,
            Token = accessToken,
        });
    }
}