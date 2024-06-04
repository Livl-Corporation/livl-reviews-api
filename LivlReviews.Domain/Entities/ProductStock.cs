namespace LivlReviews.Domain.Entities;

public class ProductStock
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public string AdminId { get; set; }
    public IUser Admin { get; set; }
    
    public int ImportId { get; set; }
    public Import Import { get; set; }
}