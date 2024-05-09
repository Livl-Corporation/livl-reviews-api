namespace LivlReviews.Email;

public interface IEmailContentService
{
    string GenerateAccountInvitationContent(string name, string activationLink);
}