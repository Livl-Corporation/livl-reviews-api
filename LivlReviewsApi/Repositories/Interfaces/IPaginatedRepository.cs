using LivlReviewsApi.Data;

namespace LivlReviewsApi.Repositories.Interfaces;

public interface IPaginatedRepository<T> : IRepository<T> where T : class
{
    PaginatedResult<T> GetPaginated(PaginationParameters paginationParameters);
    PaginatedResult<T> GetPaginated(Func<T, bool> predicate, PaginationParameters paginationParameters);
}