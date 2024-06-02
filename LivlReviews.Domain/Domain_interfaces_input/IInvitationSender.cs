using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IInvitationSender
{
    public Task SendInvitation(string senderUserId, string email);
}