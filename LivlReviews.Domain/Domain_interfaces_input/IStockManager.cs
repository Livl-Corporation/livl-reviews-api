using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Domain_interfaces_input;

public interface IStockManager
{
    public bool IsRequestable(Product product, IUser requester);
    public Task<Request> RequestProduct(Product product, IUser requester, string? message = null);
    public Request ApproveRequest(Request request, IUser requester, string? message = null);
    public Request UpdateRequestState(Request request, RequestState state);
    public void UpdateStocks(Product[] products, Import import);
    public void RemoveStocksFromPreviousImport(Import import);

}