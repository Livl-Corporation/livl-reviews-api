namespace LivlReviews.Domain.Models;

public class PaginationParameters
{
    public int page { get; set; }
    public int pageSize { get; set; }
}

public class PaginationMetadata : PaginationParameters
{
    public int total { get; set; }
    public int totalPages { get; set; }
}

public class PaginatedResult<T> where T : class
{
    public List<T> Results { get; set; }
    public PaginationMetadata Metadata { get; set; }
}