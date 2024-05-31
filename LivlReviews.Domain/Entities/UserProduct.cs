namespace LivlReviews.Domain.Entities;

public class UserProduct
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}