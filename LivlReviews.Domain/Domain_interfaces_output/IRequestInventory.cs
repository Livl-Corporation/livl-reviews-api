using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IRequestInventory
{
    bool IsRequestable(int productId, string adminId);
    Request CreateProductRequest(Request request);
    void UpdateRequestState(Request request, RequestState state);
    List<Request> GetSimilarPendingRequests(Request request);
    
    Request ApproveRequest(Request request);
    Request RejectRequest(Request request);
}