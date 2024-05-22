using LivlReviewsApi.Data;
using LivlReviewsApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviewsApi.Controllers ;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IPaginatedRepository<Product> repository;
    
    public ProductController(IPaginatedRepository<Product> repository)
    {
        this.repository = repository;
    }
    
    [HttpGet]
    public ActionResult<PaginatedResult<Product>> GetProducts(int page = 1, int pageSize = 10, string search = "", int? category = null)
    {
        PaginationParameters paginationParameters = new PaginationParameters { page = page, pageSize = pageSize };
        
        PaginatedResult<Product> products = this.repository.GetPaginated(
            product => (category == null || product.CategoryId == category) && product.Name.Contains(search), 
            paginationParameters);
        return Ok(products);
    }
}