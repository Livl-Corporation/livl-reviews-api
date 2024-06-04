using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Models;

namespace LivlReviews.Domain.Test.Spies;

public class NotificationManagerSpy : INotificationManager
{
    public bool IsSendAccountInvitationNotificationCalled { get; private set; }
    public bool IsSendRequestFromUserToAdminNotificationCalled { get; private set; }
    
    public Task SendAccountInvitationNotification(List<RecipientNotificationInvitation> recipientEmailInvitations)
    {
        IsSendAccountInvitationNotificationCalled = true;
        return Task.CompletedTask;
    }

    public Task SendRequestFromUserToAdminNotification(Request request)
    {   
        IsSendRequestFromUserToAdminNotificationCalled = true;
        return Task.CompletedTask;
    }
}