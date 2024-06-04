using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Fakes;

public class FakeUser : IUser
{
    public string Id { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public bool EmailConfirmed { get; set; }
    public List<Request> SubmittedRequests { get; set; }
    public List<Request> ReceivedRequests { get; set; }
    public List<ProductStock> Stocks { get; set; }
    public InvitationToken? InvitedByToken { get; set; }
    public int? InvitedByTokenId { get; set; }
    public List<InvitationToken> CreatedInvitationTokens { get; set; }
}