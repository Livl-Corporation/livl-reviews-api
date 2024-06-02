using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public record User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public bool isConfirmed { get; set; }
    public ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
    public ICollection<ProductRequest> ProductRequests { get; set; } = new List<ProductRequest>();

    public bool isAdmin()
    {
        return this.Role == Role.Admin;
    }
}