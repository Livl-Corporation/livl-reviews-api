﻿using LivlReviews.Email;
using Microsoft.Extensions.Options;
using Moq;

namespace LivlReviewsApiTests;

public class EmailServiceTests
{
    private readonly Mock<IEmailContentService> _emailContentServiceMock;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        Mock<IOptions<SmtpSettings>> smtpSettingsMock = new();
        _emailContentServiceMock = new Mock<IEmailContentService>();

        smtpSettingsMock.Setup(s => s.Value).Returns(new SmtpSettings
        {
            Server = "localhost",
            Port = 1025,
            Username = "user",
            Password = "password",
            EnableSsl = false,
            SenderEmail = "noreply@yourapp.com"
        });

        _emailService = new EmailService(smtpSettingsMock.Object, _emailContentServiceMock.Object);
    }

    [Fact]
    public async Task SendAccountInvitationEmailAsync_SendsEmail()
    {
        // Arrange
        var recipient = new RecipientEmailInvitation
        {
            Name = "John Doe",
            ActivationLink = "https://example.com/activate",
            Email = "john.doe@example.com"
        };
        var recipients = new List<RecipientEmailInvitation> { recipient };

        _emailContentServiceMock.Setup(s => s.GenerateAccountInvitationContent(recipient.Name, recipient.ActivationLink))
            .Returns("Email content");

        // Act
        await _emailService.SendAccountInvitationEmailAsync(recipients);

        // Assert
        _emailContentServiceMock.Verify(s => s.GenerateAccountInvitationContent(recipient.Name, recipient.ActivationLink), Times.Once);
    }
}