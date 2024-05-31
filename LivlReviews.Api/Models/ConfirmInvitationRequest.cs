namespace LivlReviews.Api.Models;

public class ConfirmInvitationRequest
{
    public required string Token { get; set; }
    public required string Password { get; set; }
}