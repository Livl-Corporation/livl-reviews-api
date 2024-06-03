using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IStockManager
{
    public bool IsRequestable(Product product, IUser requester);
    public Request RequestProduct(Product product, IUser requester);
    public Request ApproveRequest(Request request, IUser requester);
    public void UpdateRequestState(Request request, RequestState state);
}