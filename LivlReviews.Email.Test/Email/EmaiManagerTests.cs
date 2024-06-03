using LivlReviews.Email;
using Moq;
using Xunit;

namespace LivlReviews.Email.Test.Email;

public class EmaiManagerTests
{
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly Mock<IEmailContentService> _emailContentServiceMock;
    private readonly EmaiManager _emaiManager;

    public EmaiManagerTests()
    {
        _emailSenderMock = new Mock<IEmailSender>();
        _emailContentServiceMock = new Mock<IEmailContentService>();
        _emaiManager = new EmaiManager(_emailSenderMock.Object, _emailContentServiceMock.Object);
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
        await _emaiManager.SendAccountInvitationEmailAsync(recipients);

        // Assert
        _emailContentServiceMock.Verify(s => s.GenerateAccountInvitationContent(recipient.ActivationLink), Times.Once);
        _emailSenderMock.Verify(s => s.SendEmailAsync(recipient.Email, emailSubject, emailContent), Times.Once);
    }
}
