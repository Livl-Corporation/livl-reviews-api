using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Users;

public interface IUserInventory
{
    public Task<User> GetUserById(string userId);
}