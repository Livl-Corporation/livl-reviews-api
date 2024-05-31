using LivlReviews.Domain.Entities.Interfaces;

namespace LivlReviews.Domain.Entities;

public class Product : ICreatedDate, IUpdatedDate, IDeletedDate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string URL { get; set; }
    public string VinerURL { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
    public ICollection<ProductRequest> ProductRequests { get; set; } = new List<ProductRequest>();
}