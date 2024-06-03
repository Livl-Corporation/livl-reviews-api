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
    
    public Request ApproveRequest(Request request)
    {
        request.State = RequestState.Approved;
        
        ProductStock stock = stockRepository
            .GetBy(stock => stock.ProductId == request.ProductId && stock.AdminId == request.AdminId).First();

        stockRepository.Delete(stock);
        
        return requestRepository.Update(request);
    }
    
    public Request RejectRequest(Request request)
    {
        request.State = RequestState.Rejected;
        
        return requestRepository.Update(request);
    }
    
    public void UpdateRequestState(Request request, RequestState state)
    {
        request.State = state;
        requestRepository.Update(request);
    }
}