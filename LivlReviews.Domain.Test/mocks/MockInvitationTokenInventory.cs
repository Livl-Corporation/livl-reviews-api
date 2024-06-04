using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.mocks;

public class MockInvitationTokenInventory(List<InvitationToken> invitationTokens): IInvitationTokenInventory
{
    public Task<InvitationToken> GetToken(string token)
    {
        var foundToken = invitationTokens.FirstOrDefault(invitationToken => invitationToken.Token == token);
        if(foundToken is null)
        {
            throw new Exception("This token do not exist : " + token);
        }

        return Task.FromResult(foundToken);
    }
    
    public InvitationToken GetByInvitedUserId(string invitedUserId)
    {
        var foundToken = invitationTokens.FirstOrDefault(invitationToken => invitationToken.InvitedUserId == invitedUserId);
        if(foundToken is null)
        {
            throw new Exception("This token do not exist for this user : " + invitedUserId);
        }

        return foundToken;
    }

    public Task<List<InvitationToken>> GetTokensByAdminId(string adminId)
    {
        var foundTokens = invitationTokens.Where(invitationToken => invitationToken.InvitedByUserId == adminId).ToList();
        return Task.FromResult(foundTokens);
    }

    public InvitationToken Add(InvitationToken invitationToken)
    {
        invitationTokens.Add(invitationToken);
        return invitationToken;
    }
}