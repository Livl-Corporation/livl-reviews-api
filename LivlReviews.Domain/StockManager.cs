using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain;

public class StockManager(IRequestInventory requestInventory)
{
    public bool IsRequestable(Product product, User requester)
    {
        if (requester.InvitedById is null)
        {
            return false;
        }   
        
        return requestInventory.IsRequestable(product.Id, requester.InvitedById);
    }

    public Request RequestProduct(Product product, User requester)
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
}