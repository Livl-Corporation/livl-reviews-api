using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IInvitationDelivery
{
    public void DeliverInvitation(string senderUsierId, User invitedUser);
}