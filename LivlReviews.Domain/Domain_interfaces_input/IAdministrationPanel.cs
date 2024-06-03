using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IAdministrationPanel
{
    public Task<List<IUser?>> GetAdminUsers(string adminUserId);
    public Task<IUser> EnableUser(string adminUserId, string userId);
    public Task<IUser> DisableUser(string adminUserId, string userId);

}