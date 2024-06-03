using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IStockManager
{
    public bool IsRequestable(Product product, IUser requester);
    public Request RequestProduct(Product product, IUser requester);
    public Request ApproveRequest(Request request, IUser requester);
}