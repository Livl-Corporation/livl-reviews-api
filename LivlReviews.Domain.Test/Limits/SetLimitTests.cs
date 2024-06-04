using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Test.Fakes;
using LivlReviews.Domain.Test.Stubs;
using Xunit;

namespace LivlReviews.Domain.Test.Limits;

public class SetLimitTests
{
    [Fact]
    public void Should_Set_Max_Requests_If_User_Is_Admin()
    {
        // Arrange
        var user = UsersStub.Admin;
        var newLimit = 5;
        var limitConfig = new LimitConfig
        {
            AdminUser = user,
            AdminUserId = user.Id,
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
        limitsManager.SetMaxRequests(5, user.Id);
        
        // Assert
        Assert.Equal(newLimit, limitConfigsInventory.GetLimitsByAdminId(user.Id).MaxRequests);
    }
}