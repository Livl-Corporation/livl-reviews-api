using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Domain_interfaces_output;

public interface IImportInventory
{
    public Import GetOrCreateActualImport();
    public Import UpdateImport(Import import);
}