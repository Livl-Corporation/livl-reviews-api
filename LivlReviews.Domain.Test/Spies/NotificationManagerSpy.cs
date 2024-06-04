using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Email;

namespace LivlReviews.Domain.Test.Spies;

public class NotificationManagerSpy : INotificationManager
{
    
    public bool IsSendAccountInvitationNotificationCalled { get; private set; }
    public Task SendAccountInvitationNotification(List<RecipientNotificationInvitation> recipientEmailInvitations)
    {
        IsSendAccountInvitationNotificationCalled = true;
        return Task.CompletedTask;
    }
}