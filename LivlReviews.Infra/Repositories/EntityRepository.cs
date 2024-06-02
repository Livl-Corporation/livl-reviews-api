using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LivlReviews.Infra.Repositories;

public class EntityRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext DbContext;
    protected readonly DbSet<T> DbSet;
    
    public EntityRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }
    public T? GetById(int id)
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

    public T GetByFirstOrDefault(Func<T, bool> predicate)
    {
        var res = DbSet.Where(predicate).ToList().First();
        if (res is null) return null;
        return res;
    }
    public T GetByFirstOrDefault(Func<T, bool> predicate, T def)
    {
        var res = DbSet.Where(predicate).ToList().First();
        if (res is null) return def;
        return res;
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