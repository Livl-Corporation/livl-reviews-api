namespace LivlReviews.Email;

public class RecipientEmailInvitation
{
    public required string Name { get; set; }
    public required string ActivationLink { get; set; }
    public required string Email { get; set; }
}