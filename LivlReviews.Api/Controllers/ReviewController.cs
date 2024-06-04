using LivlReviews.Api.Attributes;
using LivlReviews.Api.Models;
using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ReviewController(
    IPaginatedRepository<Request> requestRepository, 
    IReviewManager reviewManager) : ControllerBase
{
    [HttpPost]
    [UserIdClaim]
    public async Task<ActionResult> CreateReview([FromBody] PostReviewRequest reviewRequest)
    {
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null) return Unauthorized();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var request = requestRepository
            .GetAndInclude(r => r.Id == reviewRequest.RequestId, ["Product", "User", "Admin"]).FirstOrDefault();
        
        if (request == null)
        {
            return NotFound("Request not found");
        }
        
        var review = new Review
        {
            RequestId = request.Id,
            Rating = reviewRequest.Rating,
            Content = reviewRequest.Content,
            Title = reviewRequest.Title
        };
        
        try 
        {
            review = reviewManager.CreateReview(review, request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok(review);
    }
}