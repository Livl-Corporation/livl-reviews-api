using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Administration;

public class AdministrationPanel(IInvitationTokenInventory invitationTokenInventory, IUserInventory userInventory): IAdministrationPanel
{
    public async Task<List<IUser?>> GetAdminUsers(string adminUserId)
    {
        IUser? adminUser = await userInventory.GetUserById(adminUserId);
        
        if(adminUser is null) 
        {
            throw new Exception("Admin user not found");
        }
        
        if(!adminUser.IsAdmin)
        {
            throw new Exception("Only admins can get their users");
        }
        
        List<InvitationToken> invitationTokens = await invitationTokenInventory.GetTokensByAdminId(adminUserId);
         
        List<string> invitedUserIds = invitationTokens.Select(invitationToken => invitationToken.InvitedUserId).ToList();
        
        List<IUser?> invitedUsers = new List<IUser?>();

        foreach (var userId in invitedUserIds)
        {
            var user = await userInventory.GetUserById(userId);
            invitedUsers.Add(user);
        }        

        return invitedUsers;
    }

    public Task<IUser> EnableUser(string adminUserId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IUser> DisableUser(string adminUserId, string userId)
    {
        throw new NotImplementedException();
    }
}