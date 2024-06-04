namespace LivlReviews.Domain.Exceptions;

public class UserAlreadyInvitedException() : Exception("User with the same email is already invited")
{
    
}