using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Exceptions;
using LivlReviews.Domain.Invitation;
using LivlReviews.Domain.Test.mocks;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Invitations;

public class InvitationTests
{
    [Fact]
    public async void Should_Send_Invitation_When_Sender_Is_Admin()
    {
        // Arrange
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory([]);

        IUser sender = UsersStub.Admin;
        IUserInventory userInventory = new StubUserInventory([sender]);
        
        IInvitationSender invitationSender = new InvitationSender(invitationTokenInventory, userInventory);

        string invitedEmail = "invitedUser@email.com";
        
        // Act
        await invitationSender.SendInvitation(sender.Id, invitedEmail);
        
        // Assert
        var newUser = await userInventory.GetUserByEmail(invitedEmail);
        Assert.NotNull(newUser);
        Assert.Equal(newUser.Role, Role.User);
        
        var invitationToken = invitationTokenInventory.GetByInvitedUserId(newUser.Id);
        Assert.NotNull(invitationToken);
        Assert.Equal(sender.Id, invitationToken.InvitedByUserId);
        Assert.Equal(newUser.Id, invitationToken.InvitedUserId);
    }

    [Fact]
    public async void Should_Throw_Exception_When_Sender_Is_Not_Admin()
    {
        // Arrange
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory([]);

        IUser sender = UsersStub.User;
        IUserInventory userInventory = new StubUserInventory([sender]);
        IInvitationSender invitationSender = new InvitationSender(invitationTokenInventory, userInventory);
        string invitedEmail = "invitedUser@email.com";

        // Act
        Task Act () => invitationSender.SendInvitation(sender.Id, invitedEmail);

        // Assert
        await Assert.ThrowsAsync<UserNotAdministratorException>(Act);

        var newUser = await userInventory.GetUserByEmail(invitedEmail);
        Assert.Null(newUser);
    }

    [Fact]
    public async void Should_Throw_Exception_When_Email_Already_Invited()
    {
        // Arrange
        IUserInventory userInventory = new StubUserInventory([
            UsersStub.User,
            UsersStub.Admin,
        ]);
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory([]);

        IInvitationSender invitationSender = new InvitationSender(
            invitationTokenInventory,
            userInventory
        );

        // Act
        Task Act () => invitationSender.SendInvitation(UsersStub.Admin.Id, UsersStub.User.Email);

        // Assert
        await Assert.ThrowsAsync<UserAlreadyInvitedException>(Act);
    }

}