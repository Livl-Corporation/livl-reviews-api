using LivlReviews.Domain.Models;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    
    public PaginatedResult<T> GetPaginated(Func<T, bool> predicate, PaginationParameters paginationParameters, string[] include)
    {
        int total = DbSet.Count(predicate);
        int totalPages = (int)Math.Ceiling((double)total / paginationParameters.pageSize);
        var query = DbSet.AsQueryable();
        
        foreach (var inc in include)
        {
            query = query.Include(inc);
        }
        
        List<T> results = query.Where(predicate)
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
}