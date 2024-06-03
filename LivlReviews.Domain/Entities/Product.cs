using LivlReviews.Domain.Entities.Interfaces;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public class Product : ICreatedDate, IUpdatedDate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string URL { get; set; }
    public string SourcePage { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }

    public List<ProductStock> Stocks { get; set; } = new List<ProductStock>();
    public List<Request> Requests { get; set; } = new List<Request>();
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public bool HaveInStock(IUser admin)
    {
        if(admin.Role < Role.Admin) return false;
        
        return Stocks.Any(stock => stock.AdminId == admin.Id);
    }
    
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