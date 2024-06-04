using LivlReviews.Domain.Domain_interfaces_output;

namespace LivlReviews.Email;

public class EmailManager(INotificationSender emailSender, INotificationContent emailContent) : INotificationManager
{
    public async Task SendAccountInvitationNotification(List<RecipientNotificationInvitation> recipientEmailInvitations)
    {
        var subject = "Vous êtes invité à rejoindre LivlReviews!";

        foreach (var recipient in recipientEmailInvitations)
        {
            var message = emailContent.GenerateAccountInvitationTokenContent(recipient.ActivationLink);
            await emailSender.SendNotification(recipient.Contact, subject, message);
        }
    }
}
