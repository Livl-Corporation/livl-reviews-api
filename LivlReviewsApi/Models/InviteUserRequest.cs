using System.ComponentModel.DataAnnotations;

namespace LivlReviewsApi.Data;

public class InviteUserRequest
{
    [Required]
    public string? Email { get; set; }
}