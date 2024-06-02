using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Users;

namespace LivlReviews.Domain.Administration;

public class AdministrationPanel(IInvitationTokenInventory invitationTokenInventory, IUserInventory userInventory): IAdministrationPanel
{
    public async Task<List<User?>> GetAdminUsers(string adminUserId)
    {
        User? adminUser = await userInventory.GetUserById(adminUserId);
        
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
        
        List<User?> invitedUsers = new List<User?>();

        foreach (var userId in invitedUserIds)
        {
            var user = await userInventory.GetUserById(userId);
            invitedUsers.Add(user);
        }        

        return invitedUsers;
    }

    public Task<User> EnableUser(string adminUserId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> DisableUser(string adminUserId, string userId)
    {
        throw new NotImplementedException();
    }
}