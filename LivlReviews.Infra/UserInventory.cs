using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace LivlReviews.Infra;

public class UserInventory (UserManager<User> userManager) : IUserInventory
{
    
    public async Task<IUser?> GetUserById(string userId)
    {
        var userResult = await userManager.FindByIdAsync(userId);
        
        if(userResult == null)
        {
            throw new UserNotFoundException();
        }
        
        return new User() { Id = userResult.Id, Email = userResult.Email ?? string.Empty, Role = userResult.Role, EmailConfirmed = userResult.EmailConfirmed };
    }

    public async Task<IUser> ValidateUser(string userId, string password)
    {
        var userResult = await userManager.FindByIdAsync(userId);

        if (userResult is null)
        {
            throw new UserNotFoundException();
        }

        var addPasswordResult = await userManager.AddPasswordAsync(userResult, password);
        if (addPasswordResult.Succeeded is false)
        {
            throw new Exception("Error while creating password");
        }
        
        userResult.EmailConfirmed = true;
        var updateEmailConfirmedResult = await userManager.UpdateAsync(userResult);
        
        if (updateEmailConfirmedResult.Succeeded is false)
        {
            throw new Exception("A problem occured while updating the user");
        }

        return new User
        {
            Email = userResult.Email,
            Role = userResult.Role,
            Id = userResult.Id,
            EmailConfirmed = true,
        };
    }

    public Task<IUser> CreateUserObject(IUser sender, string email)
    {
        return Task.FromResult<IUser>(new User
        {
            Email = email,
            Role = Role.User,
            InvitedById = sender.Id,
            InvitedBy = sender,
        });
    }
}