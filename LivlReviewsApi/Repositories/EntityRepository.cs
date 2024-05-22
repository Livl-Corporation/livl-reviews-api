using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using LivlReviewsApi.Data;
using LivlReviewsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LivlReviewsApi.Repositories;

public class EntityRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext DbContext;
    protected readonly DbSet<T> DbSet;
    
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
    
    public List<T> GetBy(Expression<Func<T, bool>> predicate)
    {
        return DbSet.Where(predicate).ToList();
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
    
    public bool DeleteBy(Expression<Func<T, bool>> predicate)
    {
        DbSet.RemoveRange(DbSet.Where(predicate));

        DbContext.SaveChanges();
        
        return true;
    }
    
    public bool Exists(Expression<Func<T, bool>> predicate)
    {
        return DbSet.Any(predicate);
    }
}