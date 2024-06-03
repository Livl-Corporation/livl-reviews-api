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
        IUser sender = UsersStub.Admin;
        IUserInventory userInventory = new StubUserInventory([sender]);
        
        IInvitationSender invitationSender = new InvitationSender(invitationDelivery, userInventory);
        
        // Act
        invitationSender.SendInvitation(sender.Id, "invitedUser@email.com");
        
        // Assert
       Assert.True(invitationDelivery.IsDeliverInvitationCalled);
    }

    [Fact]
    public async void Should_Throw_Exception_When_Sender_Is_Not_Admin()
    {
        // Arrange
        SpyInvitationDelivery invitationDelivery = new SpyInvitationDelivery();
        IUser sender = UsersStub.User;
        IUserInventory userInventory = new StubUserInventory([sender]);

        IInvitationSender invitationSender = new InvitationSender(invitationDelivery, userInventory);

        // Act
        Task Act () => invitationSender.SendInvitation(sender.Id, "invitedUser@email.com");

        // Assert
        await Assert.ThrowsAsync<Exception>(Act);
        Assert.False(invitationDelivery.IsDeliverInvitationCalled);
    }

}