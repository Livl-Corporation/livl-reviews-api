namespace LivlReviews.Email.Interfaces;

public interface IEmailContent
{
    string GenerateAccountInvitationContent(string activationLink);
}