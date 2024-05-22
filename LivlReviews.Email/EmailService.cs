namespace LivlReviews.Email;

public class EmailService(IEmailSender emailSender, IEmailContentService emailContentService)
{
    public async Task SendAccountInvitationEmailAsync(IEnumerable<RecipientEmailInvitation> recipientEmailInvitations)
    {
        string subject = "Account Invitation";

        foreach (var recipient in recipientEmailInvitations)
        {
            string message = emailContentService.GenerateAccountInvitationContent(recipient.Name, recipient.ActivationLink);
            await emailSender.SendEmailAsync(recipient.Email, subject, message);
        }
    }
}
