namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IInvitationSender
{
    public Task SendInvitation(string senderUserId, string email);
}