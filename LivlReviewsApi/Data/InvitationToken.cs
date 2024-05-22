using LivlReviewsApi.Data.Interfaces;

namespace LivlReviewsApi.Data;

public class InvitationToken : ICreatedDate
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string InvitedById { get; set; }
    public string InvitedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}