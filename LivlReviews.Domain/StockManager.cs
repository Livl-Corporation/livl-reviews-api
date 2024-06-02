using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class StockManager(IProductRequestInventory productRequestInventory)
{
    public void ApproveRequest(ProductRequest request)
    {
        request.State = RequestState.APPROVED;
        productRequestInventory.UpdateRequest(request);
        
        var pendingRequests = productRequestInventory.GetAllPendingRequestForProduct(request.UserProductId, request.UserId);
        
        foreach (var pendingRequest in pendingRequests)
        {
            pendingRequest.State = RequestState.REJECTED;
            productRequestInventory.UpdateRequest(pendingRequest);
        }
    }
}