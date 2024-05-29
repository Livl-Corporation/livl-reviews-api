using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.Spies;

public class SpyInvitationDelivery : IInvitationDelivery
{

    public bool IsDeliverInvitationCalled { get; private set; } = false;
    
    public void DeliverInvitation(User sender, User invitedUser)
    {
        IsDeliverInvitationCalled = true;
    }
}