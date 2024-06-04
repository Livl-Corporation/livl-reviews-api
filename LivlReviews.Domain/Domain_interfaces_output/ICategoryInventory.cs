using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface ICategoryInventory
{
    public Category CreateIfNotExists(Category category);
}