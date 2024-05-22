using LivlReviewsApi.Data;
using LivlReviewsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LivlReviewsApi.Repositories;

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