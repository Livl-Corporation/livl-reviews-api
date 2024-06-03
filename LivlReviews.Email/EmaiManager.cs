using LivlReviews.Email.Interfaces;

namespace LivlReviews.Email;

public class EmaiManager(IEmailSender emailSender, IEmailContent emailContent)
{
    public async Task SendAccountInvitationEmailAsync(IEnumerable<RecipientEmailInvitation> recipientEmailInvitations)
    {
        var subject = "Vous êtes invité à rejoindre LivlReviews!";

        foreach (var recipient in recipientEmailInvitations)
        {
            var message = emailContent.GenerateAccountInvitationContent(recipient.ActivationLink);
            await emailSender.SendEmailAsync(recipient.Email, subject, message);
        }
    }
}
