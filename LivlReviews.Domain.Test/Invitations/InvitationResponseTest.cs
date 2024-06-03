using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.mocks;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Invitations;

public class InvitationResponseTest
{
    [Fact]
    public void Should_Accept_User_Confirmation_When_TokenId_Is_Correct()
    {
        // Arrange
        List<InvitationToken> tokenList =
        [
            InvitationTokensStub.InvitationToken,
            InvitationTokensStub.InvitationToken2,
        ];
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory(tokenList);
        User user = new User { Role = Role.User, Email = "admin@email.com", Id = "1", isConfirmed = false};
        StubUserInventory userInventory = new StubUserInventory([user]);


        IInvitationConfirmator invitationConfirmator = new InvitationConfirmator(invitationTokenInventory, userInventory);

        // Act
        invitationConfirmator.ConfirmUser(InvitationTokensStub.InvitationToken.Token, "123");

        // Assert
        Assert.True(userInventory.IsValidateUserCalled);
    }
    
    [Fact]
    public async void Should_Not_Accept_User_Confirmation_When_TokenId_Is_Incorrect()
    {
        // Arrange
        List<InvitationToken> tokenList =
        [
            InvitationTokensStub.InvitationToken,
            InvitationTokensStub.InvitationToken2,
        ];
        
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory(tokenList);
        User user = new User { Role = Role.User, Email = "admin@email.com", Id = "1", isConfirmed = false};
        StubUserInventory userInventory = new StubUserInventory([user]);


        IInvitationConfirmator invitationConfirmator = new InvitationConfirmator(invitationTokenInventory, userInventory);

        // Act
        Task Act () => invitationConfirmator.ConfirmUser("badToken", "123");

        // Assert
        await Assert.ThrowsAsync<Exception>(Act);
        Assert.False(userInventory.IsValidateUserCalled);
    }
}