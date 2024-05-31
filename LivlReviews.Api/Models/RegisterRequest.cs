using System.ComponentModel.DataAnnotations;

namespace LivlReviews.Api.Models;

public class RegisterRequest
{
    public string? Email { get; set; }
    
    public string? Password { get; set; }
}