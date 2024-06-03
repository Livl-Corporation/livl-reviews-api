using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Stubs;

public class StubRequestInventory(List<ProductStock> stocks, List<Request> requests) : IRequestInventory
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
}