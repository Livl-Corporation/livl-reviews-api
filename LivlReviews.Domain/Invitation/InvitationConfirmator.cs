using LivlReviews.Domain.Users;

namespace LivlReviews.Domain;

public class InvitationConfirmator(IInvitationTokenInventory invitationTokenInventory, IUserInventory userInventory) : IInvitationConfirmator
{
    public async Task ConfirmUser(string token, string password)
    {
        var invitationToken = invitationTokenInventory.GetToken(token);
        if (invitationToken.Result is null)
        {
            throw new Exception("token not found");
        }

        await userInventory.ValidateUser(invitationToken.Result.InvitedUserId, password);
    }
}