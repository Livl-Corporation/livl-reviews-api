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
}