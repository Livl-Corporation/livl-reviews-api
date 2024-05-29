using System.ComponentModel.DataAnnotations;

namespace LivlReviews.Api.Models;

public class InviteRequest
{
    [Required]
    public string? Email { get; set; }
}