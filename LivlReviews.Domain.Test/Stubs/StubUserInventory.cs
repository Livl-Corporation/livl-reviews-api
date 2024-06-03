using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Stubs;

public class StubUserInventory(List<IUser> users) : IUserInventory
{
    public bool IsValidateUserCalled = false;
    
    public Task<IUser?> GetUserById(string userId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }

    public Task<IUser> ValidateUser(string userId, string password)
    {
        IsValidateUserCalled = true;
        var user = users.FirstOrDefault(u => u.Id == userId);
        if(user is null) throw new Exception("User not found");
        user.EmailConfirmed = true;
        return Task.FromResult(user);
    }

    public Task<IUser> CreateUserObject(IUser sender, string email)
    {
        return Task.FromResult<IUser>(new FakeUser
        {
            Email = email,
            Role = Role.User,
            InvitedById = sender.Id,
            InvitedBy = sender,
        });
    }
}