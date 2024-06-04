using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra;

public class ImportInventory(IRepository<Import> importRepository) : IImportInventory
{
    public Import GetOrCreateActualImport()
    {
        var import = importRepository.GetBy(i => i.FinishedAt == null).Last();
        if (import == null)
        {
            import = new Import();
            importRepository.Add(import);
        }
        
        return import;
    }
    
    public Import UpdateImport(Import import)
    {
        return importRepository.Update(import);
    }
}