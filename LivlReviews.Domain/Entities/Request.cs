using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities.Interfaces;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public class Request(IClock clock) : ICreatedDate, IUpdatedDate
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    public IUser User { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public string AdminId { get; set; }
    public IUser Admin { get; set; }
    
    public string? UserMessage { get; set; }
    public string? AdminMessage { get; set; }
    
    public RequestState State { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public DateTime? ReviewableAt { get; set; }
    
    public bool IsReviewable => State == RequestState.Received && ReviewableAt <= clock.Now;
    
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

    public string GetRelevantUserId(IUser user)
    {
        return user.IsAdmin ? AdminId : UserId;
    }
}