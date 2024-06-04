using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface INotificationContent
{
    public string GenerateAccountInvitationTokenContent(string activationLink);
    public string GenerateRequestFromUserToAdminContent(Request request);
}