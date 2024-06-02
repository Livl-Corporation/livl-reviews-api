using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IInvitationTokenInventory
{
    public Task<InvitationToken> GetToken(string token);
}