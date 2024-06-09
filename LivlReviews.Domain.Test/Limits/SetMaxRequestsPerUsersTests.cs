using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Exceptions;
using LivlReviews.Domain.Test.Fakes;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Limits;

public class SetMaxRequestsPerUsersTests
{
    [Fact]
    public void Should_Set_Max_Requests_Per_User_If_User_Admin()
    {
        // Arrange
        var adminUser = UsersStub.Admin;
        var newLimit = 5;
        var limitConfig = new LimitConfig
        {
            AdminUser = adminUser,
            AdminUserId = adminUser.Id,
            MaxRequests = 10,
            Id = 1,
            IsDisabled = false,
            MaxRequestsPerUser = 10
        };

        FakeLimitsConfigsInventory limitConfigsInventory = new FakeLimitsConfigsInventory([
            limitConfig
        ]);
        FakeUserInventory userInventory = new FakeUserInventory([adminUser]);
        ILimitsManager limitsManager = new LimitsManager(limitConfigsInventory, userInventory);

        // Act
        limitsManager.SetMaxRequestsPerUsers(adminUser.Id, newLimit);

        // Assert
        Assert.Equal(newLimit, limitConfigsInventory.GetLimitsByAdminId(adminUser.Id).MaxRequestsPerUser);
    }
    
    [Fact]
    public void Should_Throw_If_Requester_Not_Admin()
    {
        // Arrange
        var user = UsersStub.User;
        var newLimit = 5;
        var limitConfig = new LimitConfig
        {
            AdminUser = UsersStub.Admin,
            AdminUserId = UsersStub.Admin.Id,
            MaxRequests = 10,
            Id = 1,
            IsDisabled = false,
            MaxRequestsPerUser = 10
        };

        FakeLimitsConfigsInventory limitConfigsInventory = new FakeLimitsConfigsInventory([
            limitConfig
        ]);
        FakeUserInventory userInventory = new FakeUserInventory([user]);
        ILimitsManager limitsManager = new LimitsManager(limitConfigsInventory, userInventory);

        // Act 
        Task Act () => limitsManager.SetMaxRequestsPerUsers(user.Id, newLimit);
        
        // Assert
        Assert.ThrowsAsync<UserNotAdministratorException>(Act);
    }
    
    [Fact]
    public void Should_Throw_If_User_Not_Found()
    {
        // Arrange
        var user = UsersStub.User;
        var newLimit = 5;
        var limitConfig = new LimitConfig
        {
            AdminUser = UsersStub.Admin,
            AdminUserId = UsersStub.Admin.Id,
            MaxRequests = 10,
            Id = 1,
            IsDisabled = false,
            MaxRequestsPerUser = 10
        };

        FakeLimitsConfigsInventory limitConfigsInventory = new FakeLimitsConfigsInventory([
            limitConfig
        ]);
        FakeUserInventory userInventory = new FakeUserInventory([user]);
        ILimitsManager limitsManager = new LimitsManager(limitConfigsInventory, userInventory);

        // Act 
        Task Act () => limitsManager.SetMaxRequestsPerUsers("not_found", newLimit);
        
        // Assert
        Assert.ThrowsAsync<UserNotFoundException>(Act);
    }
}