using LivlReviews.Domain.Entities.Interfaces;

namespace LivlReviews.Domain.Entities;

public class Product : ICreatedDate, IUpdatedDate, IDeletedDate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string URL { get; set; }
    public string VinerURL { get; set; }
    
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}