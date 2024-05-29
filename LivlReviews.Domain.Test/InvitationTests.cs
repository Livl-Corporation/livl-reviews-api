using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Spies;
using Xunit;

namespace LivlReviews.Domain.Test;

public class InvitationTests
{
    [Fact]
    public void Should_Send_Invitation_When_Sender_Is_Admin()
    {
        // Arrange
        SpyInvitationDelivery invitationDelivery = new SpyInvitationDelivery();
        
        IInvitationSender invitationSender = new InvitationSender(invitationDelivery);
        User sender = new User { Id = "1", Email = "sender@email.com", Role = Role.Admin };
        
        // Act
        invitationSender.SendInvitation(sender, "invitedUser@email.com");
        
        // Assert
       Assert.True(invitationDelivery.IsDeliverInvitationCalled);
    }

    [Fact]
    public void Should_Throw_Exception_When_Sender_Is_Not_Admin()
    {
        // Arrange
        SpyInvitationDelivery invitationDelivery = new SpyInvitationDelivery();

        IInvitationSender invitationSender = new InvitationSender(invitationDelivery);
        User sender = new User { Id = "1", Email = "sender@email.com", Role = Role.User };

        // Act
        void Act() => invitationSender.SendInvitation(sender, "invitedUser@email.com");

        // Assert
        Assert.Throws<Exception>(Act);
        Assert.False(invitationDelivery.IsDeliverInvitationCalled);
    }

}