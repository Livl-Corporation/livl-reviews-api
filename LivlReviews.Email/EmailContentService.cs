namespace LivlReviews.Email;

public class EmailContentService : IEmailContentService
{
    public string GenerateAccountInvitationContent(string name, string activationLink)
    {
        return $@"
            <h1>Welcome to LivlReviews!</h1>
            <p>Dear {name},</p>
            <p>We are excited to have you on board. To get started with LivlReviews, please click the link below to set your password and activate your account.</p>
            <a href='{activationLink}'>Activate your account</a>
            <p>If you have any questions, feel free to reply to this email.</p>
            <p>Best,</p>
            <p>The LivlReviews Team</p>
        ";
    }
}