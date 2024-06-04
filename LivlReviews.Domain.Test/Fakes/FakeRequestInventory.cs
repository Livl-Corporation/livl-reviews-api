using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Fakes;

public class FakeRequestInventory(List<ProductStock> stocks, List<Request> requests) : IRequestInventory
{
    public List<ProductStock> stocks = stocks;
    public List<Request> requests = requests;

    public bool IsRequestable(int productId, string adminId)
    {
        return stocks.Any(stock => stock.ProductId == productId && stock.AdminId == adminId);
    }

    public Request CreateProductRequest(Request request)
    {
        return request;
    }

    public void UpdateRequestState(Request request, RequestState state)
    {
        var idx = requests.FindIndex(r => r.Id == request.Id);
        requests[idx].State = state;
    }

    public List<Request> GetSimilarPendingRequests(Request request)
    {
        return requests.FindAll(r => r.ProductId == request.ProductId && r.AdminId == request.AdminId && r.State == RequestState.Pending && r.Id != request.Id);
    }

    public Request ApproveRequest(Request request)
    {
        var idx = requests.FindIndex(r => r.Id == request.Id);
        requests[idx].State = RequestState.Approved;
        return requests[idx];
    }

    public Request RejectRequest(Request request)
    {
        var idx = requests.FindIndex(r => r.Id == request.Id);
        requests[idx].State = RequestState.Rejected;
        return requests[idx];
    }
    
    public void RemoveStock(Request request)
    {
        var idx = stocks.FindIndex(s => s.ProductId == request.ProductId && s.AdminId == request.AdminId);
        stocks.RemoveAt(idx);
    }
}