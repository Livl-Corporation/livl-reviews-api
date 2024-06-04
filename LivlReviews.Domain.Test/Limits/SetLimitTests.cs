using LivlReviews.Domain.Test.Stubs;

namespace LivlReviews.Domain.Test.Limits;

public class SetLimitTests
{
    public void Should_Set_Max_Requests_If_User_Is_Admin()
    {
        // Arrange
        var user = UsersStub.Admin;
        var newLimit = 5;
        
        ILimitsRepository limitsRepository = new FakeLimitsRepository();
        ILimitsManager limitsManager = new ILimitsManager(limitsRepository);
        
        // Act
        limitsManager.SetMaxRequests(5);
        
        // Assert
        limitsRepository.GetMaxRequests().Should().Be(5);
    }
}