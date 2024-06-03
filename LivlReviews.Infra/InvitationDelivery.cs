using System.Net.Mail;
using LivlReviews.Domain;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using InvitationToken = LivlReviews.Infra.Data.InvitationToken;
using DomainUser = LivlReviews.Domain.Entities.User;
using User = LivlReviews.Infra.Data.User;

namespace LivlReviews.Infra;

public class InvitationDelivery(UserManager<Data.User> userManager, IRepository<InvitationToken> invitationTokenRepository) : IInvitationDelivery
{
    
    public async Task DeliverInvitation(string senderUserId, DomainUser invitedUser)
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
        
        var invitationTokenResult =  invitationTokenRepository.Add(invitationToken);

        User userWithToken = newUser;
        userWithToken.InvitedByTokenId = invitationTokenResult.Id;
        await userManager.UpdateAsync(userWithToken);
        
        // TODO : We should send the email from there too :)
    }
}