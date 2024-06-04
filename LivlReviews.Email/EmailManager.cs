using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Models;

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

    public async Task SendRequestFromUserToAdminNotification(Request request)
    {
        var subject = $"[ADMIN] Demande de produit - {request.User.Email}";
        var message = emailContent.GenerateRequestFromUserToAdminContent(request);
        await emailSender.SendNotification(request.Admin.Email, subject, message);
    }
}
