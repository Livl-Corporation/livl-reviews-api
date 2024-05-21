namespace LivlReviewsApi.Data;

public class Pagination
{
    public int page { get; set; }
    public int pageSize { get; set; }
}

public class PaginationMetadata : Pagination
{
    public int total { get; set; }
    public int totalPages { get; set; }
}

