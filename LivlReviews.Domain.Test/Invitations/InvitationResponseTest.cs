using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.mocks;
using LivlReviews.Domain.Test.Stubs;
using LivlReviews.Domain.Users;
using Xunit;

namespace LivlReviews.Domain.Test;

public class InvitationResponseTest
{
    [Fact]
    public void Should_Accept_User_Confirmation_When_TokenId_Is_Correct()
    {
        // Arrange
        List<InvitationToken> tokenList = new List<InvitationToken>();
        tokenList.Add(new InvitationToken
        {
            Token = "invitationToken",
            InvitedUserId = "invitedUserId",
            InvitedByUserId = "invitedByUserId",
            Id = 0
        });
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory(tokenList);
        User user = new User { Role = Role.User, Email = "admin@email.com", Id = "1", isConfirmed = false};
        StubUserInventory userInventory = new StubUserInventory(user);


        IInvitationConfirmator invitationConfirmator = new InvitationConfirmator(invitationTokenInventory, userInventory);

        // Act
        invitationConfirmator.ConfirmUser("invitationToken", "123");

        // Assert
        Assert.True(userInventory.IsValidateUserCalled);
    }
    
    [Fact]
    public async void Should_Not_Accept_User_Confirmation_When_TokenId_Is_Incorrect()
    {
        // Arrange
        List<InvitationToken> tokenList = new List<InvitationToken>();
        tokenList.Add(new InvitationToken
        {
            Token = "invitationToken",
            InvitedUserId = "invitedUserId",
            InvitedByUserId = "invitedByUserId",
            Id = 0
        });
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory(tokenList);
        User user = new User { Role = Role.User, Email = "admin@email.com", Id = "1", isConfirmed = false};
        StubUserInventory userInventory = new StubUserInventory(user);


        IInvitationConfirmator invitationConfirmator = new InvitationConfirmator(invitationTokenInventory, userInventory);

        // Act
        Task Act () => invitationConfirmator.ConfirmUser("bad", "123");

        // Assert
        await Assert.ThrowsAsync<Exception>(Act);
        Assert.False(userInventory.IsValidateUserCalled);
    }
}