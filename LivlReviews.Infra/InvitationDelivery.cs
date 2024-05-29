using System.Net.Mail;
using LivlReviews.Domain;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using User = LivlReviews.Domain.Entities.User;

namespace LivlReviews.Infra;

public class InvitationDelivery(UserManager<Data.User> userManager, IRepository<InvitationToken> invitationTokenRepository) : IInvitationDelivery
{
    public UserManager<Data.User> UserManager = userManager;
    public IRepository<InvitationToken> InvitationTokenRepository = invitationTokenRepository;
    
    public async void DeliverInvitation(string senderUserId, User invitedUser)
    {
        var invitedUserResult = await UserManager.CreateAsync(MapUser(invitedUser));
        
        if(!invitedUserResult.Succeeded)
        {
            throw new Exception("Could not create user");
        }
        
        // Maybe this should be generated in the domain... Let's see later :)
        var randomToken = Guid.NewGuid().ToString();
        
        var invitationToken = new InvitationToken
        {
            Token = randomToken,
            InvitedByUderId = senderUserId,
            InvitedUserId = invitedUser.Id,
        };
        
        InvitationTokenRepository.Add(invitationToken);
    }
    
    private Data.User MapUser(User user)
    {
        return new Data.User
        {
            Id = user.Id,
            Email = user.Email,
            UserName = new MailAddress(user.Email).User,
        };
    }
}