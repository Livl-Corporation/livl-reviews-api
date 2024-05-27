using LivlReviews.Domain.Models;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;

namespace LivlReviews.Infra.Repositories;

public class PaginatedEntityRepository<T>(AppDbContext context) : EntityRepository<T>(context), IPaginatedRepository<T>
    where T : class
{
    public PaginatedResult<T> GetPaginated(Func<T, bool> predicate , PaginationParameters paginationParameters)
    {
        int total = DbSet.Count(predicate);
        int totalPages = (int)Math.Ceiling((double)total / paginationParameters.pageSize);
        List<T> results = DbSet.Where(predicate)
            .Skip((paginationParameters.page - 1) * paginationParameters.pageSize)
            .Take(paginationParameters.pageSize)
            .ToList();

        return new PaginatedResult<T>
        {
            Results = results,
            Metadata = new PaginationMetadata
            {
                page = paginationParameters.page,
                pageSize = paginationParameters.pageSize,
                total = total,
                totalPages = totalPages
            }
        };
    }

    public PaginatedResult<T> GetPaginated(PaginationParameters paginationParameters)
    {
        return GetPaginated(arg => true, paginationParameters);
    }
}