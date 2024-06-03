namespace LivlReviews.Email;

public interface IEmailContentService
{
    string GenerateAccountInvitationContent(string activationLink);
}