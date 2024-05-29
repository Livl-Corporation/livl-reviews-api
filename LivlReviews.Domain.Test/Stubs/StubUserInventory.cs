using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Users;

namespace LivlReviews.Domain.Test.Stubs;

public class StubUserInventory(User user) : IUserInventory
{
    private readonly User _user = user;
    public Task<User> GetUserById(string userId)
    {
        return Task.FromResult(_user);
    }
}