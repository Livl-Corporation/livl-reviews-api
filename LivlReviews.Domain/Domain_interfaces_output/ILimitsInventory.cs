namespace LivlReviews.Domain.Domain_interfaces_output;

public interface ILimitsInventory
{
    public void SetMaxRequests(int maxRequests, string adminUserId);
    public void SetMaxRequestsPerUser(int maxRequestsPerUser, string adminUserId);
    public void SetIsDisabled(bool isDisabled, string adminUserId);
    public void CreateLimits(string adminUserId, int maxRequests, int maxRequestsPerUser, bool isDisabled);
}