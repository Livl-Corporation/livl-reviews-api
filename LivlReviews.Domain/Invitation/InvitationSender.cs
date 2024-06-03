using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Invitation;

public class InvitationSender(IInvitationDelivery invitationDelivery, IUserInventory userInventory) : IInvitationSender
{
    private readonly IInvitationDelivery _invitationDelivery = invitationDelivery;
    private readonly IUserInventory _userInventory = userInventory;

    public async Task SendInvitation(string senderUserId, string email)
    {
        IUser sender = await _userInventory.GetUserById(senderUserId);
        
        if(sender.Role != Role.Admin)
        {
            throw new Exception("Only admins can send invitations");
        }

        IUser invitedUser = await _userInventory.CreateUserObject(sender, email);
        
        // User invitedUser = new User { Email = email, Role = Role.User, InvitedById = senderUserId, InvitedBy = sender };
        
        await _invitationDelivery.DeliverInvitation(senderUserId, invitedUser);
    }

}