using LivlReviews.Domain;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra.Inventories;

public class InvitationTokenInventory(IRepository<Data.InvitationToken> invitationTokenRepository) : IInvitationTokenInventory
{
    public async Task<InvitationToken> GetToken(string token)
    {
        var result = invitationTokenRepository.GetByFirstOrDefault(
            invitationToken => invitationToken.Token == token
        );

        if (result is null)
        {
            throw new Exception("Invitation token not found");
        }

        return new InvitationToken
        {
            Token = result.Token,
            InvitedUserId = result.InvitedUserId,
            InvitedByUserId = result.InvitedUserId,
            Id = result.Id,
            CreatedAt = result.CreatedAt
        };
    }
}