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
public class RequestController(IPaginatedRepository<Request> repository, UserManager<User> userManager) : ControllerBase
{
    [HttpGet]
    public ActionResult<PaginatedResult<Request>> GetRequests(int page = 1, int pageSize = 10)
    {
        PaginationParameters paginationParameters = new PaginationParameters { page = page, pageSize = pageSize };
        
        // TODO : add filter ?
        PaginatedResult<Request> requests = repository.GetPaginated(
            paginationParameters
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
    public ActionResult<Request> ApproveRequest(int id)
    {
        var user = userManager.FindByNameAsync(User.Identity.Name).Result;
        if(user is null)
        {
            return Unauthorized();
        }

        if (!Domain.Entities.Request.Can(user.Role, Operation.UPDATE))
        {
            return Forbid();
        }
        
        Request request = repository.GetById(id);
        if (request == null)
        {
            return NotFound();
        }
        
        
        
        return Ok(request);
    }
}