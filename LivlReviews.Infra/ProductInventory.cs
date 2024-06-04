using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class ProductInventory(IRepository<Product> repository) : IProductInventory
{
    public Product CreateOrUpdate(Product product)
    {
        Product? existingProduct = repository.GetBy(p => p.URL == product.URL).FirstOrDefault();
        
        if (existingProduct is not null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Image = product.Image;
            existingProduct.SourcePage = product.SourcePage;
            existingProduct.CategoryId = product.CategoryId;
            return repository.Update(existingProduct);
        }
        
        return repository.Add(product);
    }
}