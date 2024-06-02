using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain;

public interface IAdministrationPanel
{
    public Task<List<User?>> GetAdminUsers(string adminUserId);
    public Task<User> EnableUser(string adminUserId, string userId);
    public Task<User> DisableUser(string adminUserId, string userId);

}