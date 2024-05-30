namespace LivlReviews.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<Category> Children { get; set; }
    public Category Parent { get; set; }
    public int? ParentId { get; set; }
    public List<Product> Products { get; set; } 

}