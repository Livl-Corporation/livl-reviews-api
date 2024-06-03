using LivlReviews.Api.Attributes;
using LivlReviews.Domain;
using LivlReviews.Domain.Administration;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Users;
using LivlReviews.Infra;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InvitationToken = LivlReviews.Infra.Data.InvitationToken;
using User = LivlReviews.Infra.Data.User;

namespace LivlReviews.Api.Controllers;

[ApiController]
[Route("/[controller]")]
public class AdministrationController(UserManager<User> userManager, IRepository<InvitationToken> invitationTokenRepository) : ControllerBase
{
    [HttpGet("users")]
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
}