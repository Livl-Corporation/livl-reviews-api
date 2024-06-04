using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.Fakes;

public class FakeLimitsConfigsInventory(List<LimitConfig> limitConfigs): ILimitsInventory
{
    public void SetMaxRequests(int maxRequests, string adminUserId)
    {
        var limitConfig = limitConfigs.FirstOrDefault(lc => lc.AdminUserId == adminUserId);
        if (limitConfig is null)
        {
            throw new Exception();
        }
        limitConfig.MaxRequests = maxRequests;
    }

    public void SetMaxRequestsPerUser(int maxRequestsPerUser, string adminUserId)
    {
        var limitConfig = limitConfigs.FirstOrDefault(lc => lc.AdminUserId == adminUserId);
        if (limitConfig is null)
        {
            throw new Exception();
        }
        limitConfig.MaxRequestsPerUser = maxRequestsPerUser;
    }

    public void SetIsDisabled(bool isDisabled, string adminUserId)
    {
        var limitConfig = limitConfigs.FirstOrDefault(lc => lc.AdminUserId == adminUserId);
        if (limitConfig is null)
        {
            throw new Exception();
        }
        limitConfig.IsDisabled = isDisabled;
    }

    public void CreateLimits(IUser adminUser, int maxRequests, int maxRequestsPerUser, bool isDisabled)
    {
        limitConfigs.Add(new LimitConfig
        {
            AdminUserId = adminUser.Id,
            MaxRequests = maxRequests,
            MaxRequestsPerUser = maxRequestsPerUser,
            IsDisabled = isDisabled,
            AdminUser = adminUser,
        });
    }

    public LimitConfig GetLimitsByAdminId(string adminUserId)
    {
        var res = limitConfigs.FirstOrDefault(lc => lc.AdminUserId == adminUserId);
        if (res is null)
        {
            throw new Exception();
        }
        return res;
    }
}