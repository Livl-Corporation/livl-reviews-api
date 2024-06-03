using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.Spies;

public class SpyInvitationDelivery : IInvitationDelivery
{

    public bool IsDeliverInvitationCalled { get; private set; } = false;

    public Task DeliverInvitation(string senderUsierId, User invitedUser)
    {
        IsDeliverInvitationCalled = true;
        return Task.CompletedTask;
    }
}