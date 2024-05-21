using LivlReviewsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LivlReviewsApi.Repositories;

public class EntityRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext DbContext;
    private readonly DbSet<T> DbSet;
    
    public EntityRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }
    public T GetById(int id)
    {
        return DbSet.Find(id);
    }

    public List<T> GetAll()
    { 
        return DbSet.ToList();
    }
    
    public List<T> GetBy(Func<T, bool> predicate)
    {
        return DbSet.Where(predicate).ToList();
    }

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
    
    public T Add(T entity)
    {
        var res = DbSet.Add(entity).Entity;
        
        DbContext.SaveChanges();
        
        return res;
    }
    
    public void AddRange(List<T> entities)
    {
        DbSet.AddRange(entities);
        
        DbContext.SaveChanges();
    }

    public T Update(T entity)
    {
        var res = DbSet.Update(entity).Entity;
        
        DbContext.SaveChanges();
        
        return res;
    }

    public bool Delete(T entity)
    {
        DbSet.Remove(entity);

        DbContext.SaveChanges();
        
        return true;
    }
    
    public bool DeleteBy(Func<T, bool> predicate)
    {
        DbSet.RemoveRange(DbSet.Where(predicate));

        DbContext.SaveChanges();
        
        return true;
    }
}