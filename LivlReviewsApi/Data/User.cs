namespace LivlReviewsApi.Data;

public class User
{
    public int Id { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public string? Description { get; init; }
    public string? AvatarUri { get; init; }
    public required string Role { get; init; }
    public required DateTime CreatedOn { get; init; }
}