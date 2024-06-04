using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface INotificationSender
{
    public Task SendNotification(string contact, string title, string message);
}