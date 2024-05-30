using System.Net.Mail;
using LivlReviews.Domain;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using User = LivlReviews.Domain.Entities.User;

namespace LivlReviews.Infra;

public class InvitationDelivery(UserManager<Data.User> userManager, IRepository<InvitationToken> invitationTokenRepository) : IInvitationDelivery
{
    private readonly UserManager<Data.User> _userManager = userManager;
    private readonly IRepository<InvitationToken> _invitationTokenRepository = invitationTokenRepository;
    
    public async Task DeliverInvitation(string senderUserId, User invitedUser)
    {
        var newUser = new Data.User
        {
            Email = invitedUser.Email,
            UserName = new MailAddress(invitedUser.Email).User,
            Role = invitedUser.Role,
        };
        var userResult = await _userManager.CreateAsync(newUser);
        
        if(!userResult.Succeeded)
        {
            throw new Exception("Could not create user");
        }
        
        // Maybe this should be generated in the domain... Let's see later :)
        var randomToken = Guid.NewGuid().ToString();
        var invitationToken = new InvitationToken
        {
            Token = randomToken,
            InvitedByUserId = senderUserId,
            InvitedUserId = newUser.Id,
        };
        
        _invitationTokenRepository.Add(invitationToken);
    }
}