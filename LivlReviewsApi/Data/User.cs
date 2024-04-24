namespace LivlReviewsApi.Data;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Description { get; set; }
    public string? AvatarUri { get; set; }
    public string Role { get; set; }
    public DateTime CreatedOn { get; set; }
}