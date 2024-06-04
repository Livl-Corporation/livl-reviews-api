
using System.Linq.Expressions;

namespace LivlReviews.Infra.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    T? GetById(int id);
    List<T> GetAll();
    List<T> GetBy(Func<T, bool> predicate);
    T GetByFirstOrDefault(Func<T, bool> predicate);
    public List<T> GetAndInclude(Func<T, bool> predicate, string[] include);
    T Add(T entity);
    void AddRange(List<T> entities);
    T Update(T entity);
    bool Delete(T entity);
    bool DeleteBy(Expression<Func<T, bool>> predicate);
}