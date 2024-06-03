using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IUserInventory
{
    public Task<IUser?> GetUserById(string userId);
    public Task<IUser?> GetUserByEmail(string email);

    public Task<IUser> ValidateUser(string userId, string password);
    public Task CreateUser(IUser sender, string email, string? password = null);
    public Task<IUser> InstanciateUserObject(IUser sender, string email);
    public Task UpdateUser(IUser user);
}