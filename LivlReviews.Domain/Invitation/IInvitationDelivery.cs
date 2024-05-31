using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IInvitationDelivery
{
    public Task DeliverInvitation(string senderUserId, User invitedUser);
}