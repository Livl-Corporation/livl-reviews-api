using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Test.Fakes;

namespace LivlReviews.Domain.Test.Stubs;

public class UsersStub
{
    public static FakeUser User = new FakeUser
    {
        Email = "jose@email.fr",
        EmailConfirmed = true,
        Role = Role.User,
        Id = "1"
    };

    public static FakeUser User2 = new FakeUser
    {
        Email = "isamet@email.fr",
        EmailConfirmed = true,
        Role = Role.User,
        Id = "2"
    };
    public static FakeUser User3 = new FakeUser
    {
        Email = "romain@email.fr",
        EmailConfirmed = true,
        Role = Role.User,
        Id = "3"
    };
    public static FakeUser User4 = new FakeUser
    {
        Email = "arthur@email.fr",
        EmailConfirmed = true,
        Role = Role.User,
        Id = "4"
    };
    public static FakeUser User5 = new FakeUser
    {
        Email = "lovan@email.fr",
        EmailConfirmed = true,
        Role = Role.User,
        Id = "5"
    };
    public static FakeUser User6 = new FakeUser
    {
        Email = "samyyy@email.fr",
        EmailConfirmed = true,
        Role = Role.User,
        Id = "6"
    };
    
    public static FakeUser Admin = new FakeUser
    {
        Email = "ravael@email.fr",
        EmailConfirmed = true,
        Role = Role.Admin,
        Id = "7"
    };
    
    public static FakeUser Admin2 = new FakeUser
    {
        Email = "vulien@email.fr",
        EmailConfirmed = true,
        Role = Role.Admin,
        Id = "8"
    };
    
    
}