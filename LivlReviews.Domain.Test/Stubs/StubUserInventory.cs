using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Stubs;

public class StubUserInventory(List<User> users) : IUserInventory
{
    public bool IsValidateUserCalled = false;
    
    public Task<User?> GetUserById(string userId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }

    public Task<User> ValidateUser(string userId, string password)
    {
        IsValidateUserCalled = true;
        var user = users.FirstOrDefault(u => u.Id == userId);
        if(user is null) throw new Exception("User not found");
        return Task.FromResult(user with {isConfirmed = true});
    }
}