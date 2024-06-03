using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class StockManager(IRequestInventory requestInventory) : IStockManager
{
    public bool IsRequestable(Product product, IUser requester)
    {
        if (requester.InvitedById is null)
        {
            return false;
        }   
        
        return requestInventory.IsRequestable(product.Id, requester.InvitedById);
    }

    public Request RequestProduct(Product product, IUser requester)
    {
        if(!IsRequestable(product, requester))
        {
            throw new Exception("This product is not requestable by this user.");
        }
        
        Request request = new Request
        {
            ProductId = product.Id,
            AdminId = requester.InvitedById,
            UserId = requester.Id,
            State = RequestState.Pending
        };
        
        var res = requestInventory.CreateProductRequest(request);

        return res;
    }

    public Request ApproveRequest(Request request, IUser requester)
    {
        List<Request> requests = requestInventory.GetSimilarPendingRequests(request);

        foreach (Request req in requests)
        {
            requestInventory.RejectRequest(req);
        }
        
        return requestInventory.ApproveRequest(request);
    }
}