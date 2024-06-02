using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Stubs;

public class StubUserInventory(User user) : IUserInventory
{
    public bool IsValidateUserCalled = false;
    
    public Task<User> GetUserById(string userId)
    {
        return Task.FromResult(user);
    }

    public Task<User> ValidateUser(string userId, string password)
    {
        IsValidateUserCalled = true;
        return Task.FromResult(user with {isConfirmed = true});
    }
}