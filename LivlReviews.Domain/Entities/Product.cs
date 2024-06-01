using LivlReviews.Domain.Entities.Interfaces;

namespace LivlReviews.Domain.Entities;

public class Product : ICreatedDate, IUpdatedDate, IDeletedDate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string URL { get; set; }
    public string SourcePage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}