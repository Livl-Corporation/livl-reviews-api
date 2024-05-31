using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Users;

namespace LivlReviews.Domain.Invitation;

public class InvitationSender(IInvitationDelivery invitationDelivery, IUserInventory userInventory) : IInvitationSender
{
    private readonly IInvitationDelivery _invitationDelivery = invitationDelivery;
    private readonly IUserInventory _userInventory = userInventory;

    public async Task SendInvitation(string senderUserId, string email)
    {
        User sender = await _userInventory.GetUserById(senderUserId);
        
        if(sender.Role != Role.Admin)
        {
            throw new Exception("Only admins can send invitations");
        }
        
        User invitedUser = new User { Email = email, Role = Role.User};
        
        await _invitationDelivery.DeliverInvitation(senderUserId, invitedUser);
    }

}