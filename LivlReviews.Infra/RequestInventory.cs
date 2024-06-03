using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class RequestInventory(IRepository<Request> requestRepository, IRepository<ProductStock> stockRepository) : IRequestInventory
{
    public bool IsRequestable(int productId, string adminId)
    {
        return stockRepository.GetBy(stock => stock.AdminId == adminId && stock.ProductId == productId).Count > 0;
    }

    public Request CreateProductRequest(Request request)
    {
        return requestRepository.Add(request);
    }
    
    public void UpdateRequestState(Request request)
    {
        requestRepository.Update(request);
    }
}