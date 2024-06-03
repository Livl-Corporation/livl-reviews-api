using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test;

public class FakeUser : IUser
{
    public string Id { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string? InvitedById { get; set; }
    public IUser? InvitedBy { get; set; }
    public bool EmailConfirmed { get; set; }
    public List<Request> SubmittedRequests { get; set; }
    public List<Request> ReceivedRequests { get; set; }
    public List<ProductStock> Stocks { get; set; }
}