using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace LivlReviews.Infra;

public class UserInventory (UserManager<Data.User> userManager) : IUserInventory
{
    
    private readonly UserManager<Data.User> _userManager = userManager;

    public async Task<User> GetUserById(string userId)
    {
        var userResult = await _userManager.FindByIdAsync(userId);
        
        if(userResult == null)
        {
            throw new Exception("User not found");
        }
        
        return new User { Id = userResult.Id, Email = userResult.Email ?? string.Empty, Role = userResult.Role};
    }

    public Task<User> ValidateUser(string userId, string password)
    {
        throw new NotImplementedException();
    }
}