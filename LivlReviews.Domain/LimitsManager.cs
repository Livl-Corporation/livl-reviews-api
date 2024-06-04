using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Exceptions;

namespace LivlReviews.Domain;

public class LimitsManager(ILimitsInventory limitsInventory, IUserInventory userInventory): ILimitsManager
{
    public async void SetMaxRequests(int maxRequests, string userId)
    {
        IUser? user = await userInventory.GetUserById(userId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        if (!user.IsAdmin)
        {
            throw new UserNotAdministratorException();
        }
        
        limitsInventory.SetMaxRequests(maxRequests, userId);
    }
}