using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LivlReviews.Infra.Data;

public class User : IdentityUser, IUser
{
    public Role Role { get; set; }
    public string? InvitedById { get; set; }
    public IUser? InvitedBy { get; set; }
    public List<Request> SubmittedRequests { get; set; }
    public List<Request> ReceivedRequests { get; set; }
    public List<ProductStock> Stocks { get; set; }
    
    public bool IsAdmin => Role == Role.Admin;
}