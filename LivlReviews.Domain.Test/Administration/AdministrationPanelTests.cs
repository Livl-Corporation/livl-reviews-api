using LivlReviews.Domain.Administration;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Test.mocks;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Administration;

public class AdministrationPanelTests
{
    [Fact]
    public async void Should_Get_All_User_Invited_By_Admin()
    {
        // Arrange
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory([
            InvitationTokensStub.InvitationToken,
            InvitationTokensStub.InvitationToken2,
            InvitationTokensStub.InvitationToken3,
            InvitationTokensStub.InvitationToken4,
        ]);
        IUserInventory userInventory = new StubUserInventory([
            UsersStub.User,
            UsersStub.User2,
            UsersStub.User3,
            UsersStub.User4,
            UsersStub.Admin,
        ]);

        IAdministrationPanel administrationPanel = new AdministrationPanel(invitationTokenInventory, userInventory);

        List<IUser> expectedResult =
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

    [Fact]
    public async void Should_Get_Empty_List_When_Admin_Has_No_Invited_Users()
    {
        // Arrange
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory([
            InvitationTokensStub.InvitationToken,
        ]);
        IUserInventory userInventory = new StubUserInventory([
            UsersStub.Admin,
            UsersStub.Admin2,
        ]);

        IAdministrationPanel administrationPanel = new AdministrationPanel(invitationTokenInventory, userInventory);

        List<IUser> expectedResult = new List<IUser>();

        // Act
        var adminUsersResult = await administrationPanel.GetAdminUsers(UsersStub.Admin2.Id);

        // Assert
        Assert.Equal(expectedResult, adminUsersResult);
    }
    
    [Fact]
    public async void Should_Throw_Exception_When_Admin_User_Not_Found()
    {
        // Arrange
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory([
            InvitationTokensStub.InvitationToken
        ]);
        IUserInventory userInventory = new StubUserInventory([
            UsersStub.Admin,
        ]);

        IAdministrationPanel administrationPanel = new AdministrationPanel(invitationTokenInventory, userInventory);

        // Act
        Task Act() => administrationPanel.GetAdminUsers("badId");

        // Assert
        await Assert.ThrowsAsync<Exception>(Act);
    }

    [Fact]
    public async void Should_Throw_Exception_When_User_Is_Not_Admin()
    {
        // Arrange
        IInvitationTokenInventory invitationTokenInventory = new MockInvitationTokenInventory([
            InvitationTokensStub.InvitationToken
        ]);
        IUserInventory userInventory = new StubUserInventory([
            UsersStub.User,
            UsersStub.Admin
        ]);

        IAdministrationPanel administrationPanel = new AdministrationPanel(invitationTokenInventory, userInventory);

        // Act
        Task Act() => administrationPanel.GetAdminUsers(UsersStub.User.Id);

        // Assert
        await Assert.ThrowsAsync<Exception>(Act);
    }
}