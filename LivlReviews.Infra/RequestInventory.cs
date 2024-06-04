using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class RequestInventory(IPaginatedRepository<Request> requestRepository, IRepository<ProductStock> stockRepository) : IRequestInventory
{
    public bool IsRequestable(int productId, string adminId)
    {
        return stockRepository.GetBy(stock => stock.AdminId == adminId && stock.ProductId == productId).Count > 0;
    }

    public Request CreateProductRequest(Request request)
    {
        return requestRepository.Add(request);
    }

    public List<Request> GetSimilarPendingRequests(Request request)
    {
        return requestRepository.GetBy(req => 
            req.ProductId == request.ProductId && 
            req.UserId == request.UserId && 
            req.State == RequestState.Pending && 
            req.Id != request.Id);
    }
    
    public Request UpdateRequestState(Request request, RequestState state)
    {
        request.State = state;
        requestRepository.Update(request);
        return request;
    }
    
    public void RemoveStock(Request request)
    {
        ProductStock stock = stockRepository
            .GetBy(stock => stock.ProductId == request.ProductId && stock.AdminId == request.AdminId).First();

        stockRepository.Delete(stock);
    }
}