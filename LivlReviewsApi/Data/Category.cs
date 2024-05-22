namespace LivlReviewsApi.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // relations
    public Category? Parent { get; set; }
    public int? ParentId { get; set; }
    
    public List<Category> Children { get; set; }
    
    public List<Product> Products { get; set; }
}