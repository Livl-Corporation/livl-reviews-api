using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class CategoryInventory(IRepository<Category> repo) : ICategoryInventory
{
    public Category CreateIfNotExists(Category category)
    {
        var existingCategory = repo.GetById(category.Id);

        if (existingCategory is not null) return existingCategory;
        
        return repo.Add(category);
    }
}