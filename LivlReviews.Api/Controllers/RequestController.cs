using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Models;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RequestController(IPaginatedRepository<Request> repository) : ControllerBase
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
}