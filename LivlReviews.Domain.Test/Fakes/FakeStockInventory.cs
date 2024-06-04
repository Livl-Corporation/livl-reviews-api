using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.Fakes;

public class FakeStockInventory(List<ProductStock> stocks) : IStockInventory
{
    public ProductStock CreateOrUpdate(ProductStock productStock)
    {
        var idx = stocks.FindIndex(s => s.ProductId == productStock.ProductId && s.AdminId == productStock.AdminId);
        if (idx == -1)
        {
            stocks.Add(productStock);
        }
        else
        {
            stocks[idx] = productStock;
        }
        return productStock;
    }

    public void RemoveStocksFromPreviousImports(Import import)
    {
        stocks.RemoveAll(s => s.ImportId == import.Id);
    }
}