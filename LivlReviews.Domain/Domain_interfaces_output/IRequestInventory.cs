using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IRequestInventory
{
    bool IsRequestable(int productId, string adminId);
    Request CreateProductRequest(Request request);
    void UpdateRequestState(Request request);
}