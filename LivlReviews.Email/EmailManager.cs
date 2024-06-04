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
    
    public async Task SendNotificationToUserAboutRequestStateChange(Request request)
    {
        var subject = $"[{request.Product.Name}] - Changement d'état de votre demande de produit";
        var message = emailContent.GenerateRequestStateChangeContent(request);
        Console.WriteLine($"Sending email to {request.User.Email} with subject: {subject}");
        await emailSender.SendNotification(request.User.Email, subject, message);
    }
    
    public async Task SendNotificationToAdminWhenReviewSubmitted(Review review)
    {
        var subject = $"[{review.Request.Product.Name}] - Nouvelle review reçue";
        var message = emailContent.GenerateReceivedReviewContent(review);
        await emailSender.SendNotification(review.Request.Admin.Email, subject, message);
    }
}
