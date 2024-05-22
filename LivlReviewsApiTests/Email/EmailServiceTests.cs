using LivlReviews.Email;
using Moq;

namespace LivlReviewsApiTests.Email;

public class EmailServiceTests
{
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly Mock<IEmailContentService> _emailContentServiceMock;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        _emailSenderMock = new Mock<IEmailSender>();
        _emailContentServiceMock = new Mock<IEmailContentService>();
        _emailService = new EmailService(_emailSenderMock.Object, _emailContentServiceMock.Object);
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
        
        var emailContent = "Email content";
        var emailSubject = "Account Invitation";

        _emailContentServiceMock.Setup(s => s.GenerateAccountInvitationContent(recipient.Name, recipient.ActivationLink))
            .Returns(emailContent);

        // Act
        await _emailService.SendAccountInvitationEmailAsync(recipients);

        // Assert
        _emailContentServiceMock.Verify(s => s.GenerateAccountInvitationContent(recipient.Name, recipient.ActivationLink), Times.Once);
        _emailSenderMock.Verify(s => s.SendEmailAsync(recipient.Email, emailSubject, emailContent), Times.Once);
    }
}
