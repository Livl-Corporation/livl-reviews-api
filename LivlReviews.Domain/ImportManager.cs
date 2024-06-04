using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public class ImportManager(IStockManager stockmanager, IImportInventory importInventory, ICategoryInventory categoryInventory, IProductInventory productInventory) : IImportManager
{
    public Import ImportProducts(Category parentCategory, Category childCategory, Product[] products)
    {
        Import import = importInventory.GetOrCreateActualImport();
        
        ImportCategories(parentCategory, childCategory);
        
        foreach (Product product in products)
        {
            product.CategoryId = childCategory.Id;
            productInventory.CreateOrUpdate(product);
        }
        
        stockmanager.UpdateStocks(products, import);
        
        return import;
    }
    
    void ImportCategories(Category parentCategory, Category childCategory)
    {
        childCategory.ParentId = parentCategory.Id;
        
        categoryInventory.CreateIfNotExists(parentCategory);
        categoryInventory.CreateIfNotExists(childCategory);
    }
    
    public void EndImport(Import import)
    {
        import.FinishedAt = DateTime.Now;
        importInventory.UpdateImport(import);
        
        
    }
}