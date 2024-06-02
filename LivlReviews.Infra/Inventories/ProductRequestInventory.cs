using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra.Inventories;

public class ProductRequestInventory(IRepository<ProductRequest> repository) : IProductRequestInventory
{
    public List<ProductRequest> GetAllPendingRequestForProduct(int productId, string userId)
    {
        return repository.GetBy(pr => pr.UserProduct.ProductId == productId && pr.UserProduct.UserId == userId && pr.State == RequestState.PENDING); 
    }

    public void UpdateRequest(ProductRequest request)
    {
        repository.Update(request);
    }
}