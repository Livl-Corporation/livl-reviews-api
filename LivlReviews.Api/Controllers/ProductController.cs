using System.Security.Claims;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Models;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController(
    IPaginatedRepository<Product> paginatedRepository,
    IRepository<ProductRequest> productRequestRepository) : ControllerBase
{
    
    [HttpGet]
    public ActionResult<PaginatedResult<Product>> GetProducts(int page = 1, int pageSize = 10, string search = "")
    {
        PaginationParameters paginationParameters = new PaginationParameters { page = page, pageSize = pageSize };
        PaginatedResult<Product> products = paginatedRepository.GetPaginated(product => product.Name.Contains(search), paginationParameters);
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = paginatedRepository.GetById(id);
        
        if (product == null)
        {
            return NotFound();
        }
        
        return Ok(product);
    }
    
    [HttpPost("request")]
    public ActionResult RequestProduct([FromBody] ProductRequest productRequestBody)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var product = paginatedRepository.GetById(productRequestBody.ProductId);

        if (product == null)
        {
            return NotFound(new { message = "Product not found." });
        }
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if(userId == null)
        {
            return Unauthorized("User not found.");
        }

        var productRequest = new ProductRequest
        {
            UserId = userId,
            ProductId = product.Id,
            RequestedAt = DateTime.UtcNow
        };

        productRequestRepository.Add(productRequest);

        return Ok(new { message = "Product requested successfully." });
    }
    
}