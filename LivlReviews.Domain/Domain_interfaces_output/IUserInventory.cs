using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IUserInventory
{
    public Task<IUser?> GetUserById(string userId);

    public Task<IUser> ValidateUser(string userId, string password);
    public Task<IUser> CreateUserObject(IUser sender, string email);
}