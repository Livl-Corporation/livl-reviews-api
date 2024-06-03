using LivlReviews.Api.Attributes;
using LivlReviews.Api.Models;
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
    public async Task<ActionResult<PaginatedResult<Request>>> GetRequests(int page = 1, int pageSize = 10, RequestState? state = null)
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
            request => request.GetRelevantUserId(currentUser) == currentUserId && (state == null || request.State == state),
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
    public async Task<ActionResult<Request>> ApproveRequest(int id, [FromBody] MessageRequest messageRequest)
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

        stockManager.ApproveRequest(request, currentUser, messageRequest.Message);
        
        return Ok(request);
    }
    
    [HttpPost("{id}/reject")]
    [UserIdClaim]
    public async Task<ActionResult<Request>> RejectRequest(int id, [FromBody] MessageRequest messageRequest)
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

        request.State = RequestState.Rejected;
        request.AdminMessage = messageRequest.Message;
        
        repository.Update(request);
        
        return Ok(request);
    }
}