using System.Net.Mail;
using LivlReviews.Domain;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Email;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using User = LivlReviews.Infra.Data.User;

namespace LivlReviews.Infra;

public class InvitationDelivery(UserManager<User> userManager, IRepository<InvitationToken> invitationTokenRepository, IEmailSender emailSender,IEmailContentService emailContentService) : IInvitationDelivery
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
        
        var emailService = new EmaiManager(emailSender, emailContentService);
        var recipientEmailInvitations = new List<RecipientEmailInvitation>
        {
            new RecipientEmailInvitation
            {
                Email = newUser.Email,
                // TODO : get real app url
                ActivationLink = $"https://localhost:3000/auth/password?token={randomToken}"
            }
        };
        await emailService.SendAccountInvitationEmailAsync(recipientEmailInvitations);
        
    }
}