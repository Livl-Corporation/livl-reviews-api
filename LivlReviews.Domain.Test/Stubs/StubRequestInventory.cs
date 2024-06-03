using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.Stubs;

public class StubRequestInventory(ProductStock stock) : IRequestInventory
{
    public bool IsRequestable(int productId, string adminId)
    {
        return stock.ProductId == productId && stock.AdminId == adminId;
    }

    public Request CreateProductRequest(Request request)
    {
        return request;
    }
    
    public void UpdateRequestState(Request request)
    {
        request.State = request.State;
    }
}