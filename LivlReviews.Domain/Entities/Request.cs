using LivlReviews.Domain.Entities.Interfaces;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public class Request : ICreatedDate, IUpdatedDate
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public string AdminId { get; set; }
    public User Admin { get; set; }
    
    public RequestState State { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static bool Can(Role role, Operation operation)
    {
        switch (operation)
        {
            case Operation.READ:
            case Operation.CREATE:
                return role >= Role.User;
            
            default:
                return role >= Role.Admin;
        }
    }
}