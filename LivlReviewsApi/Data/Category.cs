namespace LivlReviewsApi.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // relations
    public int? ParentId { get; set; }
    public Category? Parent;

    public List<Category> Children;

    public List<Product> Products;
}