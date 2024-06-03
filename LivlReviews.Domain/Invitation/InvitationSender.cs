using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Invitation;

public class InvitationSender(IInvitationDelivery invitationDelivery, IUserInventory userInventory) : IInvitationSender
{

    public async Task SendInvitation(string senderUserId, string email)
    {
        IUser? sender = await userInventory.GetUserById(senderUserId);
        
        if(sender is null)
        {
            throw new Exception("User not found");
        }
        
        if(sender.Role != Role.Admin)
        {
            throw new Exception("Only admins can send invitations");
        }

        IUser invitedUser = await userInventory.CreateUserObject(sender, email);
        
        // User invitedUser = new User { Email = email, Role = Role.User, InvitedById = senderUserId, InvitedBy = sender };
        
        await invitationDelivery.DeliverInvitation(senderUserId, invitedUser);
    }

}