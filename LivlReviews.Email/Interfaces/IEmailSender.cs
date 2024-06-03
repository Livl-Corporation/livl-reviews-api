namespace LivlReviews.Email.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}