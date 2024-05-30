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
    IRepository<Product> repository
    ) : ControllerBase
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
        Product product = repository.GetById(id);
        return Ok(product);
    }
    
}