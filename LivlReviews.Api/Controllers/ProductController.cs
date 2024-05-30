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
    IPaginatedRepository<Product> repository
    ) : ControllerBase
{
    [HttpGet]
    public ActionResult<PaginatedResult<Product>> GetProducts(int page = 1, int pageSize = 10, string search = "", int? category = null)
    {
        PaginationParameters paginationParameters = new PaginationParameters { page = page, pageSize = pageSize };
        
        PaginatedResult<Product> products = repository.GetPaginated(
            product => (category == null || product.CategoryId == category) && product.Name.Contains(search), 
            paginationParameters
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
    
}