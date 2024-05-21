using LivlReviewsApi.Data;

namespace LivlReviewsApi.Repositories;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    List<T> GetAll();
    List<T> GetBy(Func<T, bool> predicate);
    PaginatedResult<T> GetPaginated(PaginationParameters paginationParameters);
    PaginatedResult<T> GetPaginated(Func<T, bool> predicate, PaginationParameters paginationParameters);
    T Add(T entity);
    void AddRange(List<T> entities);
    T Update(T entity);
    bool Delete(T entity);
    bool DeleteBy(Func<T, bool> predicate);
}