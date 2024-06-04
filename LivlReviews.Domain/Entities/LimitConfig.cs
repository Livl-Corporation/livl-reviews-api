namespace LivlReviews.Domain.Entities;

public class LimitConfig
{
    public int Id { get; set; }
    public required string AdminUserId { get; set; }
    public required IUser AdminUser { get; set; }
    public int MaxRequests { get; set; }
    public int MaxRequestsPerUser { get; set; }
    public bool IsDisabled { get; set; }
}