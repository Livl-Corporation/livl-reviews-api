namespace LivlReviews.Api.Models;

public class ConfirmInvitationRequest
{
    public string? Token { get; set; }
    public string? Password { get; set; }
}