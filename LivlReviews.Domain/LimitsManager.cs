using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Exceptions;

namespace LivlReviews.Domain;

public class LimitsManager(ILimitsInventory limitsInventory, IUserInventory userInventory): ILimitsManager
{
    public async Task SetMaxRequests(string userId, int maxRequests)
    {
        IUser? adminUser = await userInventory.GetUserById(userId);
        if (adminUser is null)
        {
            throw new UserNotFoundException();
        }
        if (!adminUser.IsAdmin)
        {
            throw new UserNotAdministratorException();
        }
        
        limitsInventory.SetMaxRequests(maxRequests, userId);
    }
}