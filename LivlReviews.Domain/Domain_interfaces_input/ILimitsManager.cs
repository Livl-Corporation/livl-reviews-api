namespace LivlReviews.Domain.Domain_interfaces_input;

public interface ILimitsManager
{
    public void SetMaxRequests(int maxRequests, string userId);
}