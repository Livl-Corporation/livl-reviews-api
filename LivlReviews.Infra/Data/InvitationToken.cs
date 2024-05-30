using LivlReviews.Domain.Entities.Interfaces;

namespace LivlReviews.Infra.Data;

public class InvitationToken : ICreatedDate
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string InvitedByUserId { get; set; }
    public string InvitedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}