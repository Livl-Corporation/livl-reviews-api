using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace LivlReviews.Email;

public class EmailService(IOptions<SmtpSettings> smtpSettings, IEmailContentService emailContentService)
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

    public async Task SendAccountInvitationEmailAsync(IEnumerable<RecipientEmailInvitation> recipientEmailInvitation)
    {
        string subject = "Account Invitation";
        
        foreach (var recipient in recipientEmailInvitation)
        {
            string message = emailContentService.GenerateAccountInvitationContent(recipient.Name, recipient.ActivationLink);
            await SendEmailAsync(recipient.Email, subject, message);       
        }
    }

    private async Task SendEmailAsync(string email, string subject, string message)
    {
        using var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = _smtpSettings.EnableSsl
        };

        try
        {
            await client.SendMailAsync(
                new MailMessage(_smtpSettings.SenderEmail, email, subject, message)
                {
                    IsBodyHtml = true
                }
            );
        }
        catch (SmtpException ex)
        {
            throw new Exception($"Failed to send email to {email}. Error: {ex.Message}");
        }       
    }
}
