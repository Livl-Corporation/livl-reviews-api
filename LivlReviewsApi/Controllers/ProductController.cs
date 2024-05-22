using LivlReviewsApi.Data;
using LivlReviewsApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviewsApi.Controllers ;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IRepository<Product> repository;
    
    public ProductController(IRepository<Product> repository)
    {
        this.repository = repository;
    }
    
    [HttpGet]
    public ActionResult<PaginatedResult<Product>> GetProducts(int page = 1, int pageSize = 10, string search = "")
    {
        PaginationParameters paginationParameters = new PaginationParameters { page = page, pageSize = pageSize };
        PaginatedResult<Product> products = this.repository.GetPaginated(product => product.Name.Contains(search), paginationParameters);
        return Ok(products);
    }
}