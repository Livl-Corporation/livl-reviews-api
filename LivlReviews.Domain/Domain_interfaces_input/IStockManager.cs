using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IStockManager
{
    public bool IsRequestable(Product product, User requester);
    public Request RequestProduct(Product product, User requester);
}