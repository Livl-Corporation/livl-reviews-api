using LivlReviewsApi.Data;
using LivlReviewsApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlReviewsApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IRepository<Category> repository;
    
    public CategoryController(IRepository<Category> repository)
    {
        this.repository = repository;
    }    
    
    [HttpGet]
    public ActionResult<Category[]> GetCategories()
    {
        List<Category> categories = this.repository.GetAll();

        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategory(int id)
    {
        Category category = this.repository.GetById(id);

        if(category is null)
            return NotFound();
        
        return Ok(category);
    }
    
    [HttpPost]
    public ActionResult<Category> AddCategory([FromBody] Category category)
    {
        Category newCategory = this.repository.Add(category);

        return Ok(newCategory);
    }
    
    [HttpPut]
    public ActionResult<Category> UpdateCategory([FromBody] Category modifiedCategory)
    {
        if (!this.repository.Exists(c => c.Id == modifiedCategory.Id))
            return NotFound();
        
        Category updatedCategory = this.repository.Update(modifiedCategory);

        return Ok(updatedCategory);
    }
    
    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteCategory(int id)
    {
        if (!this.repository.Exists(category => category.Id == id))
            return NotFound();
                    
        Category category = this.repository.GetById(id);
        
        bool deleted = this.repository.Delete(category);

        return Ok(deleted);
    }
}