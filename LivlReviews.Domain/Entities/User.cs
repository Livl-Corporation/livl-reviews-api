using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public record User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public bool isConfirmed { get; set; }
    
    public bool IsAdmin => Role == Role.Admin;
}