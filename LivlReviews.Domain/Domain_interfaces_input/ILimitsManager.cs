namespace LivlReviews.Domain.Domain_interfaces_input;

public interface ILimitsManager
{
    public Task SetMaxRequests(string userId, int maxRequests);
    public Task SetMaxRequestsPerUsers(string userId, int maxRequestsPerUser);
}