using LivlReviews.Domain.Entities.Interfaces;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public class ProductRequest : ICreatedDate, IUpdatedDate
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int UserProductId { get; set; }
    public UserProduct UserProduct { get; set; }
    public RequestState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}