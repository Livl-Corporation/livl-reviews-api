using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;

namespace LivlReviews.Domain.Test.Stubs;

public class UsersStub
{
    public static User User = new User
    {
        Email = "jose@email.fr",
        isConfirmed = true,
        Role = Role.User,
        Id = "1"
    };

    public static User User2 = new User
    {
        Email = "isamet@email.fr",
        isConfirmed = true,
        Role = Role.User,
        Id = "2"
    };
    public static User User3 = new User
    {
        Email = "romain@email.fr",
        isConfirmed = true,
        Role = Role.User,
        Id = "3"
    };
    public static User User4 = new User
    {
        Email = "arthur@email.fr",
        isConfirmed = true,
        Role = Role.User,
        Id = "4"
    };
    public static User User5 = new User
    {
        Email = "lovan@email.fr",
        isConfirmed = true,
        Role = Role.User,
        Id = "5"
    };
    public static User User6 = new User
    {
        Email = "samyyy@email.fr",
        isConfirmed = true,
        Role = Role.User,
        Id = "6"
    };
    
    public static User Admin = new User
    {
        Email = "ravael@email.fr",
        isConfirmed = true,
        Role = Role.Admin,
        Id = "7"
    };
    
    public static User Admin2 = new User
    {
        Email = "vulien@email.fr",
        isConfirmed = true,
        Role = Role.Admin,
        Id = "8"
    };
    
    
}