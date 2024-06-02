using LivlReviews.Domain.Administration;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.mocks;
using LivlReviews.Domain.Test.Stubs;
using LivlReviews.Domain.Users;
using Xunit;

namespace LivlReviews.Domain.Test.Administration;

public class ShouldGetAdminUsers
{
    [Fact]
    public async void Should_Get_All_User_Invited_By_Admin()
    {
        // Arrange
        List<User> users =
        [
            UsersStub.User,
            UsersStub.User2,
            UsersStub.User3,
            UsersStub.User4,
            UsersStub.Admin,
        ];
        
        List < InvitationToken > invitationTokens =
        [
            InvitationTokensStub.InvitationToken,
            InvitationTokensStub.InvitationToken2,
            InvitationTokensStub.InvitationToken3,
            InvitationTokensStub.InvitationToken4,
        ];

        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory(invitationTokens);
        IUserInventory userInventory = new StubUserInventory(users);

        IAdministrationPanel administrationPanel = new AdministrationPanel(invitationTokenInventory, userInventory);

        List<User> expectedResult =
        [
            UsersStub.User,
            UsersStub.User2,
            UsersStub.User3,
        ];

        // Act
        var adminUsersResult = await administrationPanel.GetAdminUsers(UsersStub.Admin.Id);

        // Assert
        Assert.Equal(expectedResult, adminUsersResult);
    }
}