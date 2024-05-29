using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class InvitationSender(IInvitationDelivery invitationDelivery) : IInvitationSender
{
    public IInvitationDelivery InvitationDelivery = invitationDelivery;

    public void SendInvitation(User sender, string email)
    {
        if(sender.Role != Role.Admin)
        {
            throw new Exception("Only admins can send invitations");
        }
        
        User invitedUser = new User { Email = email, Role = Role.User};

        InvitationDelivery.DeliverInvitation(sender, invitedUser);
    }

}