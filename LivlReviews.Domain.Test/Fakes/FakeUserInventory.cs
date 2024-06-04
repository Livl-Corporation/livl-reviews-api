using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Fakes;

public class FakeUserInventory(List<IUser> users) : IUserInventory
{
    public bool IsValidateUserCalled = false;
    
    public Task<IUser?> GetUserById(string userId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }

    public Task<IUser?> GetUserByEmail(string email)
    {
        var user = users.FirstOrDefault(u => u.Email == email);
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

    public Task CreateUser(IUser sender, string email, string? password = null)
    {
        var newUser = new FakeUser
        {
            Email = email,
            Role = Role.User,
            InvitedByTokenId = sender.InvitedByTokenId,
            InvitedByToken = sender.InvitedByToken,
        };
        users.Add(newUser);
        return Task.CompletedTask;
    }

    public Task<IUser> InstanciateUserObject(IUser sender, string email)
    {
        return Task.FromResult<IUser>(new FakeUser
        {
            Email = email,
            Role = Role.User,
            InvitedByTokenId = sender.InvitedByTokenId,
            InvitedByToken = sender.InvitedByToken,
        });
    }

    public Task UpdateUser(IUser user)
    {
        var userToUpdate = users.FirstOrDefault(u => u.Id == user.Id);
        if(userToUpdate is null) throw new Exception("User not found");
        userToUpdate.EmailConfirmed = user.EmailConfirmed;
        userToUpdate.InvitedByTokenId = user.InvitedByTokenId;
        userToUpdate.InvitedByToken = user.InvitedByToken;
        return Task.CompletedTask;
    }
}