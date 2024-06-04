using System.Net;
using System.Net.Mail;
using LivlReviews.Domain.Domain_interfaces_output;
using Microsoft.Extensions.Options;

namespace LivlReviews.Email;

public class SmtpEmailSender(IOptions<SmtpSettings> smtpSettings, string password) : INotificationSender
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

    public async Task SendNotification(string contact, string title, string message)
    {
        using var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, password),
            EnableSsl = _smtpSettings.EnableSsl
        };

        try
        {
            await client.SendMailAsync(
                new MailMessage(_smtpSettings.SenderEmail, contact, title, message)
                {
                    IsBodyHtml = true
                }
            );
        }
        catch (SmtpException ex)
        {
            throw new Exception($"Failed to send email to {contact}. Error: {ex.Message}");
        }
    }
}
