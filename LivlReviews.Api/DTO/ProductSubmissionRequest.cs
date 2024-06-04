using LivlReviews.Domain.Entities;

namespace LivlReviews.Api.Models;

public class ProductSubmissionRequest
{
    public int page { get; set; }
    public int pageCount { get; set; }
    public Category pc { get; set; }
    public Category cc { get; set; }
    public Product[] data { get; set; }
}