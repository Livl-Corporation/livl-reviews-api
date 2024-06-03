using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LivlReviews.Infra.Data;

public class User : IdentityUser
{
    public Role Role { get; set; }
    
    public InvitationToken? InvitedByToken { get; set; }
    public int? InvitedByTokenId { get; set; }
    public List<InvitationToken> CreatedInvitationTokens { get; set; }
    
    public Domain.Entities.User ToDomainUser()
    {
        return new Domain.Entities.User
        {
            Id = Id,
            Email = Email,
            Role = Role,
            isConfirmed = EmailConfirmed,
        };
    }
}