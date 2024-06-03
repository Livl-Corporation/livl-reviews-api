namespace LivlReviews.Email;

public class EmaiManager(IEmailSender emailSender, IEmailContentService emailContentService)
{
    public async Task SendAccountInvitationEmailAsync(IEnumerable<RecipientEmailInvitation> recipientEmailInvitations)
    {
        string subject = "Vous êtes invité à rejoindre LivlReviews!";

        foreach (var recipient in recipientEmailInvitations)
        {
            string message = emailContentService.GenerateAccountInvitationContent(recipient.ActivationLink);
            await emailSender.SendEmailAsync(recipient.Email, subject, message);
        }
    }
}
