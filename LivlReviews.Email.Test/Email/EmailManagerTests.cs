using LivlReviews.Domain.Domain_interfaces_output;
using Moq;
using Xunit;

namespace LivlReviews.Email.Test.Email;

public class EmailManagerTests
{
    private readonly Mock<INotificationSender> _emailSenderMock;
    private readonly Mock<INotificationContent> _emailContentServiceMock;
    private readonly EmailManager _emailManager;

    public EmailManagerTests()
    {
        _emailSenderMock = new Mock<INotificationSender>();
        _emailContentServiceMock = new Mock<INotificationContent>();
        _emailManager = new EmailManager(_emailSenderMock.Object, _emailContentServiceMock.Object);
    }

    [Fact]
    public async Task SendAccountInvitationEmailAsync_SendsEmail()
    {
        // Arrange
        var recipient = new RecipientNotificationInvitation
        {
            ActivationLink = "https://example.com/activate",
            Contact = "john.doe@example.com"
        };
        var recipients = new List<RecipientNotificationInvitation> { recipient };
        
        var emailContent = "Email content";
        var emailSubject = "Vous êtes invité à rejoindre LivlReviews!";

        _emailContentServiceMock.Setup(s => s.GenerateAccountInvitationTokenContent(recipient.ActivationLink))
            .Returns(emailContent);

        // Act
        await _emailManager.SendAccountInvitationNotification(recipients);

        // Assert
        _emailContentServiceMock.Verify(s => s.GenerateAccountInvitationTokenContent(recipient.ActivationLink), Times.Once);
        _emailSenderMock.Verify(s => s.SendNotification(recipient.Contact, emailSubject, emailContent), Times.Once);
    }
}
