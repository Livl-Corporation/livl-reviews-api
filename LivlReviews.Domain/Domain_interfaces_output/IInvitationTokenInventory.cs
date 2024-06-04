using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IInvitationTokenInventory
{
    public Task<InvitationToken> GetToken(string token);
    public InvitationToken GetByInvitedUserId(string invitedUserId);
    public Task<List<InvitationToken>> GetTokensByAdminId(string adminId);
    public InvitationToken Add(InvitationToken invitationToken);
}