using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IInvitationSender
{
    public void SendInvitation(User sender, string email);
}