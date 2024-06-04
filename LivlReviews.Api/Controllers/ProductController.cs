using LivlReviews.Api.Attributes;
using LivlReviews.Api.Models;
using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Models;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController(
    IPaginatedRepository<Product> repository,
    IStockManager stockManager,
    IRepository<User> userRepository
    ) : ControllerBase
{
    [HttpGet]
    [UserIdClaim]
    public ActionResult<PaginatedResult<Product>> GetProducts(int page = 1, int pageSize = 10, string search = "", int? category = null)
    {
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null) return Unauthorized();
        
        var currentUser = userRepository.GetAndInclude(u => u.Id == currentUserId, ["InvitedByToken"]).First();
        if(currentUser is null)
        {
            return Unauthorized();
        }
        
        // TODO : check that only the products from the admin who invite the user is returned
        PaginationParameters paginationParameters = new PaginationParameters { page = page, pageSize = pageSize };
        
        PaginatedResult<Product> products = repository.GetPaginated(
            product => (category == null || product.CategoryId == category || product.Category.ParentId == category) && 
                       product.Name.Contains(search) && 
                       product.Stocks.Any(stock => stock.AdminId == currentUser.GetRelevantAdminId()), 
            paginationParameters,
            ["Stocks", "Category"]
        );
        
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = repository.GetById(id);
        
        if (product == null)
        {
            return NotFound();
        }
        
        return Ok(product);
    }
    
    [HttpPost("{id}/request")]
    [UserIdClaim]
    public async Task<ActionResult<Request>> RequestProduct(int id, [FromBody] MessageRequest messageRequest)
    {
        var currentUserId = HttpContext.Items["UserId"] as string;
        if(currentUserId is null) return Unauthorized();
        
        var currentUser = userRepository.GetAndInclude(u => u.Id == currentUserId, ["InvitedByToken"]).First();
        if(currentUser is null)
        {
            return Unauthorized();
        }
        
        var product = repository.GetById(id);        
        if(product is null)
        {
            return NotFound();
        }
        
        if (!stockManager.IsRequestable(product, currentUser))
        {
            return NotFound();
        }
        
        return Ok(stockManager.RequestProduct(product, currentUser, messageRequest.Message));
    }
}