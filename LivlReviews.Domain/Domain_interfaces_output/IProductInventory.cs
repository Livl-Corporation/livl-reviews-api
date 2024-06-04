using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IProductInventory
{
    public Product CreateOrUpdate(Product product);
}