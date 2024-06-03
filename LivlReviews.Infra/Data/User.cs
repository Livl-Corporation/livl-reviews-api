using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LivlReviews.Infra.Data;

public class User : IdentityUser, IUser
{
    public Role Role { get; set; }

    public List<Request> SubmittedRequests { get; set; }
    public List<Request> ReceivedRequests { get; set; }
    public List<ProductStock> Stocks { get; set; }
    public Domain.Entities.InvitationToken? InvitedByToken { get; set; }
    public int? InvitedByTokenId { get; set; }
    public List<InvitationToken> CreatedInvitationTokens { get; set; }
}