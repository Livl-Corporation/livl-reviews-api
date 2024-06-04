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
        
        var request = requestRepository.GetById(reviewRequest.RequestId);
        
        if (request == null)
        {
            return NotFound("Request not found");
        }
        
        var review = new Review
        {
            RequestId = reviewRequest.RequestId,
            Rating = reviewRequest.Rating,
            Content = reviewRequest.Content,
            Title = reviewRequest.Title,
        };
        
        return Ok(reviewManager.CreateReview(review));
    }
}