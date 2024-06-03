using LivlReviews.Api.Attributes;
using LivlReviews.Domain;
using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Models;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RequestController(IPaginatedRepository<Request> repository, UserManager<User> userManager, IStockManager stockManager) : ControllerBase
{
    [HttpGet]
    [UserIdClaim]
    public async Task<ActionResult<PaginatedResult<Request>>> GetRequests(int page = 1, int pageSize = 10)
    {
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null) return Unauthorized();
        
        var currentUser = await userManager.FindByIdAsync(currentUserId);
        if(currentUser is null)
        {
            return Unauthorized();
        }
        
        PaginationParameters paginationParameters = new PaginationParameters { page = page, pageSize = pageSize };
        
        PaginatedResult<Request> requests = repository.GetPaginated(
            request => currentUser.IsAdmin ? request.AdminId == currentUserId : request.UserId == currentUserId,
            paginationParameters,
            ["User", "Product"]
        );
        
        return Ok(requests);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Request> GetRequest(int id)
    {
        var request = repository.GetById(id);
        
        if (request == null)
        {
            return NotFound();
        }
        
        return Ok(request);
    }
    
    [HttpPost("{id}/approve")]
    [UserIdClaim]
    public async Task<ActionResult<Request>> ApproveRequest(int id)
    {
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null) return Unauthorized();
        
        var currentUser = await userManager.FindByIdAsync(currentUserId);
        if(currentUser is null)
        {
            return Unauthorized();
        }

        if (!Domain.Entities.Request.Can(currentUser.Role, Operation.UPDATE))
        {
            return Forbid();
        }
        
        Request request = repository.GetById(id);
        if (request == null)
        {
            return NotFound();
        }

        stockManager.ApproveRequest(request, currentUser);
        
        return Ok(request);
    }
}