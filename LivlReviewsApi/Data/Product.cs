using LivlReviewsApi.Data.Interfaces;

namespace LivlReviewsApi.Data;

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
}