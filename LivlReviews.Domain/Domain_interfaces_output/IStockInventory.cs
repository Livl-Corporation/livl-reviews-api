using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IStockInventory
{
    public ProductStock CreateOrUpdate(ProductStock productStock);
    public void RemoveStocksFromPreviousImports(Import import);
    
}