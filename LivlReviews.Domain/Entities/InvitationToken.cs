using LivlReviews.Domain.Entities.Interfaces;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Entities;

public class InvitationToken : ICreatedDate
{
    public int Id { get; set; }
    public required string Token { get; set; }
    public string? InvitedByUserId { get; set; }
    public required string InvitedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public User InvitedByUser { get; set; }
    public User InvitedUser { get; set; }
    
    public static bool Can(Role role, Operation operation)
    {
        return role >= Role.Admin;
    }
}