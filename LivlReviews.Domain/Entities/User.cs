using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public record User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string? InvitedById { get; set; }
    public User? InvitedBy { get; set; }
    public bool isConfirmed { get; set; }

    public List<Request> SubmittedRequests { get; set; } = new List<Request>();
    public List<Request> ReceivedRequests { get; set; } = new List<Request>();
    public List<ProductStock> Stocks { get; set; } = new List<ProductStock>();
    
    public bool IsAdmin()
    {
        return this.Role == Role.Admin;
    }
    
    public static bool Can(Role role, Operation operation)
    {
        return Role.Admin >= role;
    }
}