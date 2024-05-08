using LivlReviewsApi.Enums;
using Microsoft.AspNetCore.Identity;

namespace LivlReviewsApi.Data;

public class User : IdentityUser
{
    public Role Role { get; set; }
}