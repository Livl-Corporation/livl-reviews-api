using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IProductRequestInventory
{
    List<ProductRequest> GetAllPendingRequestForProduct(int productId, string userId);
    void UpdateRequest(ProductRequest request);
}