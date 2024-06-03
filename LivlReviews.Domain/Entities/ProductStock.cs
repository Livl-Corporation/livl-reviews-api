namespace LivlReviews.Domain.Entities;

public class ProductStock
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public string AdminId { get; set; }
    public IUser Admin { get; set; }
}