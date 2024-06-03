using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IInvitationDelivery
{
    public Task DeliverInvitation(string senderUserId, IUser invitedUser);
}