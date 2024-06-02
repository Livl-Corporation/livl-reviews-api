using System.Reflection.Metadata;
using LivlReviews.Domain.Entities;

namespace LivlReviews.Domain.Test.Stubs;

public class InvitationTokensStub
{
    public static InvitationToken InvitationToken = new InvitationToken
    {
        Token = "token-test-1",
        InvitedUserId = UsersStub.User.Id,
        InvitedByUserId = UsersStub.Admin.Id,
        Id = 1,
        CreatedAt = new DateTime()
    };
    public static InvitationToken InvitationToken2 = new InvitationToken
    {
        Token = "token-test-2",
        InvitedUserId = UsersStub.User2.Id,
        InvitedByUserId = UsersStub.Admin.Id,
        Id = 2,
        CreatedAt = new DateTime()
    };
    public static InvitationToken InvitationToken3 = new InvitationToken
    {
        Token = "token-test-3",
        InvitedUserId = UsersStub.User3.Id,
        InvitedByUserId = UsersStub.Admin.Id,
        Id = 1,
        CreatedAt = new DateTime()
    };
}