using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Stubs;

public class StubProductRequestInventory(List<ProductRequest> productRequests) : IProductRequestInventory
{
    public List<ProductRequest> GetAllPendingRequestForProduct(int productId, string userId)
    {
        return productRequests.Where(pr => pr.UserProduct.ProductId == productId && pr.UserProduct.UserId == userId && pr.State == RequestState.PENDING).ToList();
    }

    public void UpdateRequest(ProductRequest request)
    {
        var productRequest = productRequests.FirstOrDefault(pr => pr.Id == request.Id);
        if (productRequest != null)
        {
            productRequests.Remove(productRequest);
            productRequests.Add(request);
        }
    }
}