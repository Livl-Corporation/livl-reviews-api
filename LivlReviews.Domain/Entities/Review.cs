using System.ComponentModel.DataAnnotations;

namespace LivlReviews.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    
    [MinLength(10)]
    [MaxLength(255)]
    public string Title { get; set; }
    
    [MinLength(100)]
    [MaxLength(10000)]
    public string Content { get; set; }
    
    [Range(1, 5)]
    public int Rating { get; set; }
    
    public int RequestId { get; set; }
    
    public Request Request { get; set; }
}