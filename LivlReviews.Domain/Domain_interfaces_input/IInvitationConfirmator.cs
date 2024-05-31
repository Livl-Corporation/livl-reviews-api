namespace LivlReviews.Domain;

public interface IInvitationConfirmator
{
    public Task ConfirmUser(string token, string password);
}