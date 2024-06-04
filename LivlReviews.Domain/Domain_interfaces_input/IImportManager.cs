using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IImportManager
{
    public Import ImportProducts(Category parentCategory, Category childCategory, Product[] products);
    public void EndImport(Import import);
}