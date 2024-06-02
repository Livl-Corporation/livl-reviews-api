using System.Security.Claims;
using LivlReviews.Domain;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Models;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = LivlReviews.Infra.Data.User;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController(
    IPaginatedRepository<Product> repository,
    StockManager stockManager,
    UserManager<User> userManager
    ) : ControllerBase
{
    [HttpGet]
    public ActionResult<PaginatedResult<Product>> GetProducts(int page = 1, int pageSize = 10, string search = "", int? category = null)
    {
        // TODO : check that only the products from the admin who invite the user is returned
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
    
    [HttpPost("{id}/request")]
    public async Task<ActionResult> RequestProduct(int id)
    {
        var currentUser = await userManager.GetUserAsync(User);
        if(currentUser is null)
        {
            return Unauthorized();
        }
        
        var product = repository.GetById(id);        
        if(product is null)
        {
            return NotFound();
        }

        if (!stockManager.IsRequestable(product, currentUser.ToDomainUser()))
        {
            return NotFound();
        }
        
        return Ok(stockManager.RequestProduct(product, currentUser.ToDomainUser()));
    }
}