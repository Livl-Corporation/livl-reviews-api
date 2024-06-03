using LivlReviews.Api.Models;
using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ReviewController(
    IPaginatedRepository<Request> requestRepository, 
    UserManager<User> userManager, 
    IReviewManager reviewManager) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateReview([FromBody] PostReviewRequest reviewRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var currentUser = await userManager.GetUserAsync(User);
        if(currentUser is null)
        {
            return Unauthorized();
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