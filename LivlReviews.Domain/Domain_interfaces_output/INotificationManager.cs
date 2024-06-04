using LivlReviews.Domain.Entities;
using LivlReviews.Email;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface INotificationManager
{
    public Task SendAccountInvitationNotification(List<RecipientNotificationInvitation> recipientEmailInvitations);
}