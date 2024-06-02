using System.ComponentModel.DataAnnotations;
using LivlReviews.Domain.Entities.Interfaces;

namespace LivlReviews.Infra.Data;

public class InvitationToken : ICreatedDate
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Token { get; set; }
    [Required] 
    public string InvitedByUserId { get; set; }
    [Required]
    public string InvitedUserId { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}