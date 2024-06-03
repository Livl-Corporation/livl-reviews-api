namespace LivlReviews.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public int Rating { get; set; }
    
    public int RequestId { get; set; }
    
    public Request Request { get; set; }
}