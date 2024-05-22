namespace LivlReviewsApi.Data;

public class InvitationToken
{
    public string Token { get; set; }
    public int InvitedById { get; set; }
    public int InvitedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}