using LivlReviews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IRepository<Category> _repository;

    public CategoriesController(IRepository<Category> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAllCategories()
    {
        return Ok(_repository.GetAll());
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategory(int id)
    {
        var category = _repository.GetById(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult<Category> CreateCategory(Category category)
    {
        var createdCategory = _repository.Add(category);
        return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }
        
        // TODO : check if user is allowed to update category

        _repository.Update(category);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _repository.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        _repository.Delete(category);

        return NoContent();
    }
}
