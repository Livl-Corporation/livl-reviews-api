namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IInvitationConfirmator
{
    public Task ConfirmUser(string token, string password);
}