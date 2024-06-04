using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using User = LivlReviews.Infra.Data.User;

namespace LivlReviews.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoryController(IRepository<Category> categoriesRepository, UserManager<User> userManager) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAllCategories()
    {
        return Ok(categoriesRepository.GetBy(c => c.ParentId == null));
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategory(int id)
    {
        var category = categoriesRepository.GetById(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult<Category> CreateCategory(Category category)
    {
        IUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
        
        if (!Category.Can(user.Role, Operation.CREATE))
        {
            return Forbid();
        }
        
        var createdCategory = categoriesRepository.Add(category);
        return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }
        
        IUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
        
        if (!Category.Can(user.Role, Operation.UPDATE))
        {
            return Forbid();
        }
        
        Category res = categoriesRepository.Update(category);

        return Ok(res);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        IUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
    
        if (!Category.Can(user.Role, Operation.DELETE))
        {
            return Forbid();
        }
        
        var category = categoriesRepository.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        categoriesRepository.Delete(category);

        return NoContent();
    }
}
