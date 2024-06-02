using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Invitation;
using LivlReviews.Domain.Test.Spies;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Invitations;

public class InvitationTests
{
    [Fact]
    public void Should_Send_Invitation_When_Sender_Is_Admin()
    {
        // Arrange
        SpyInvitationDelivery invitationDelivery = new SpyInvitationDelivery();
        User user = new User { Role = Role.Admin, Email = "admin@email.com", Id = "1"};
        IUserInventory userInventory = new StubUserInventory(user);
        
        IInvitationSender invitationSender = new InvitationSender(invitationDelivery, userInventory);
        
        // Act
        invitationSender.SendInvitation("1", "invitedUser@email.com");
        
        // Assert
       Assert.True(invitationDelivery.IsDeliverInvitationCalled);
    }

    [Fact]
    public async void Should_Throw_Exception_When_Sender_Is_Not_Admin()
    {
        // Arrange
        SpyInvitationDelivery invitationDelivery = new SpyInvitationDelivery();
        User user = new User { Role = Role.User, Email = "user@email.com", Id = "1"};
        IUserInventory userInventory = new StubUserInventory(user);

        IInvitationSender invitationSender = new InvitationSender(invitationDelivery, userInventory);

        // Act
        Task Act () => invitationSender.SendInvitation("2", "invitedUser@email.com");

        // Assert
        await Assert.ThrowsAsync<Exception>(Act);
        Assert.False(invitationDelivery.IsDeliverInvitationCalled);
    }

}