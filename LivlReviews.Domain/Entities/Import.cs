using LivlReviews.Domain.Entities.Interfaces;

namespace LivlReviews.Domain.Entities;

public class Import : ICreatedDate, IUpdatedDate
{
    public int Id { get; set; }
    public string AdminId { get; set; }
    public IUser Admin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}