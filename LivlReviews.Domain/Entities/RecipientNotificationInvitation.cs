using LivlReviews.Domain.Entities;

namespace LivlReviews.Email;

public class RecipientNotificationInvitation
{
    public required string Contact { get; set; }
    public required string ActivationLink;
}