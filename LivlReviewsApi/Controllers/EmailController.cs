using LivlReviews.Email;

namespace LivlReviewsApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class EmailController(EmailService emailService) : ControllerBase
{
    [HttpPost("send-invitation-email")]
    public async Task<IActionResult> SendInvitationEmail([FromBody] RecipientEmailInvitation[] recipients)
    {
        try
        {
            await emailService.SendAccountInvitationEmailAsync(recipients);
        
            return Ok("Email sent successfully");
        } 
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
