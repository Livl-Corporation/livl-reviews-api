using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IInvitationDelivery
{
    public void DeliverInvitation(User sender, string email);
}