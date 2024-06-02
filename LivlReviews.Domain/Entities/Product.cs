using LivlReviews.Domain.Entities.Interfaces;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public class Product : ICreatedDate, IUpdatedDate, IDeletedDate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string URL { get; set; }
    public string SourcePage { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public static bool Can(Role role, Operation operation)
    {
        switch (operation)
        {
            case Operation.READ:
                return role >= Role.User;
            
            default:
                return role >= Role.Admin;
        }
    }
}