using LivlReviews.Domain.Domain_interfaces_output;

namespace LivlReviews.Email;

public class EmaiManager(SmtpEmailSender emailSender, EmailContent emailContent) : INotificationManager
{
    public async Task SendAccountInvitationNotification(List<RecipientEmailInvitation> recipientEmailInvitations)
    {
        var subject = "Vous êtes invité à rejoindre LivlReviews!";

        foreach (var recipient in recipientEmailInvitations)
        {
            var message = emailContent.GenerateAccountInvitationTokenContent(recipient.ActivationLink);
            await emailSender.SendNotification(recipient.Contact, subject, message);
        }
    }
}
