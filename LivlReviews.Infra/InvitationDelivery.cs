using System.Net.Mail;
using LivlReviews.Domain;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using InvitationToken = LivlReviews.Infra.Data.InvitationToken;

namespace LivlReviews.Infra;

public class InvitationDelivery(UserManager<User> userManager, IRepository<InvitationToken> invitationTokenRepository) : IInvitationDelivery
{
    public async Task DeliverInvitation(string senderUserId, IUser invitedUser)
    {
        var admin = await userManager.FindByIdAsync(senderUserId);
        if(admin is null)
        {
            throw new Exception("Admin not found");
        }
        
        var newUser = new User
        {
            Email = invitedUser.Email,
            UserName = new MailAddress(invitedUser.Email).User,
            Role = invitedUser.Role,
            InvitedBy = admin,
            InvitedById = invitedUser.InvitedById,
        };
        var userResult = await userManager.CreateAsync(newUser);
        
        if(!userResult.Succeeded)
        {
            throw new Exception("Could not create user");
        }
        
        var randomToken = Guid.NewGuid().ToString();
        var invitationToken = new InvitationToken
        {
            Token = randomToken,
            InvitedByUserId = senderUserId,
            InvitedUserId = newUser.Id,
        };
        
        invitationTokenRepository.Add(invitationToken);
        
        // TODO : We should send the email from there too :)
    }
}