﻿using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace LivlReviews.Email;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpSettings _smtpSettings;

    public SmtpEmailSender(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
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
