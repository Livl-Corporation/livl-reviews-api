namespace LivlReviews.Domain.Entities;

public class InvitationToken
{
    public int Id { get; set; }
    public required string Token { get; set; }
    public required string InvitedByUserId { get; set; }
    public required string InvitedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}