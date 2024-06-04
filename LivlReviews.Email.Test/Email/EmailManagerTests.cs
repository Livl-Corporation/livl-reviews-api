using LivlReviews.Email.Interfaces;
using Moq;
using Xunit;

namespace LivlReviews.Email.Test.Email;

public class EmailManagerTests
{
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly Mock<IEmailContent> _emailContentServiceMock;
    private readonly EmailManager _emailManager;

    public EmailManagerTests()
    {
        _emailSenderMock = new Mock<IEmailSender>();
        _emailContentServiceMock = new Mock<IEmailContent>();
        _emailManager = new EmailManager(_emailSenderMock.Object, _emailContentServiceMock.Object);
    }

    [Fact]
    public async Task SendAccountInvitationEmailAsync_SendsEmail()
    {
        // Arrange
        var recipient = new RecipientEmailInvitation
        {
            ActivationLink = "https://example.com/activate",
            Email = "john.doe@example.com"
        };
        var recipients = new List<RecipientEmailInvitation> { recipient };
        
        var emailContent = "Email content";
        var emailSubject = "Vous êtes invité à rejoindre LivlReviews!";

        _emailContentServiceMock.Setup(s => s.GenerateAccountInvitationContent(recipient.ActivationLink))
            .Returns(emailContent);

        // Act
        await _emailManager.SendAccountInvitationEmailAsync(recipients);

        // Assert
        _emailContentServiceMock.Verify(s => s.GenerateAccountInvitationContent(recipient.ActivationLink), Times.Once);
        _emailSenderMock.Verify(s => s.SendEmailAsync(recipient.Email, emailSubject, emailContent), Times.Once);
    }
}
