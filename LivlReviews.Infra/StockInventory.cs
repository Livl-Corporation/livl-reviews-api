using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class StockInventory(IRepository<ProductStock> repository) : IStockInventory
{
    public ProductStock CreateOrUpdate(ProductStock productStock)
    {   
        ProductStock? existingProductStock = repository.GetBy(p => p.ProductId == productStock.ProductId && p.AdminId == productStock.AdminId).FirstOrDefault();
        
        if (existingProductStock is not null)
        {
            existingProductStock.ImportId = productStock.ImportId;
            return repository.Update(existingProductStock);
        }
        
        return repository.Add(productStock);
    }
    
    public void RemoveStocksFromPreviousImports(Import import)
    {
        repository.DeleteBy(ps => ps.ImportId != import.Id);
    }
}