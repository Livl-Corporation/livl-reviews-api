using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class StockManager(IRequestInventory requestInventory, INotificationManager notificationManager) : IStockManager
{
    public bool IsRequestable(Product product, IUser requester)
    {
        if (requester.InvitedByTokenId is null || requester.InvitedByToken is null)
        {
            return false;
        }   
        
        return requestInventory.IsRequestable(product.Id, requester.InvitedByToken.InvitedByUserId);
    }

    public async Task<Request> RequestProduct(Product product, IUser requester, string? message = null)
    {
        if(!IsRequestable(product, requester))
        {
            throw new Exception("This product is not requestable by this user.");
        }
        
        Request request = new Request
        {
            ProductId = product.Id,
            AdminId = requester.InvitedByToken.InvitedByUserId,
            UserId = requester.Id,
            UserMessage = message,
            State = RequestState.Pending
        };
        
        var res = requestInventory.CreateProductRequest(request);
        
        request.Admin = requester.InvitedByToken.InvitedByUser;
        request.User = requester;
        await notificationManager.SendRequestFromUserToAdminNotification(request);
        
        return res;
    }
    
    public void UpdateRequestState(Request request, RequestState state)
    {
        requestInventory.UpdateRequestState(request, state);
    }

    public Request ApproveRequest(Request request, IUser requester, string? message = null)
    {
        List<Request> requests = requestInventory.GetSimilarPendingRequests(request);

        foreach (Request req in requests)
        {
            req.AdminMessage = "[SYSTEM] Request was rejected because a similar request from another user was approved.";
            requestInventory.RejectRequest(req);
        }
        
        request.AdminMessage = message;
        
        requestInventory.RemoveStock(request);
        return requestInventory.ApproveRequest(request);
    }
}