using System.ComponentModel.DataAnnotations;
using LivlReviewsApi.Enums;

namespace LivlReviewsApi.Data;

public class RegisterRequest
{
    [Required]
    public string? Token { get; set; }
    
    [Required]
    public string? Password { get; set; }
}