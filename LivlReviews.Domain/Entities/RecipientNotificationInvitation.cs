using LivlReviews.Domain.Entities;

namespace LivlReviews.Email;

public class RecipientEmailInvitation
{
    public required string Contact { get; set; }
    public required string ActivationLink;
}