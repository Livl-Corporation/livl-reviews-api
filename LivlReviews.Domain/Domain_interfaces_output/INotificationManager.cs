using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Models;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface INotificationManager
{
    public Task SendAccountInvitationNotification(List<RecipientNotificationInvitation> recipientEmailInvitations);
    public Task SendRequestFromUserToAdminNotification(Request request);
    public Task SendNotificationToUserAboutRequestStateChange(Request request);
}