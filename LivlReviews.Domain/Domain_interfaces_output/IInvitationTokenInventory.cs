using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IInvitationTokenInventory
{
    public Task<InvitationToken> GetToken(string token);
    public Task<List<InvitationToken>> GetTokensByAdminId(string adminId);
}