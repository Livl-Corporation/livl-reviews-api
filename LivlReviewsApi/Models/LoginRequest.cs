using System.ComponentModel.DataAnnotations;

namespace LivlReviewsApi.Data;

public class LoginRequest
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}