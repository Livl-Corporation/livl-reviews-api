using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Enums;
using LivlReviews.Domain.Exceptions;

namespace LivlReviews.Domain.Invitation;

public class InvitationSender(IInvitationTokenInventory invitationTokenInventory, IUserInventory userInventory) : IInvitationSender
{

    public async Task SendInvitation(string senderUserId, string email)
    {
        IUser? sender = await userInventory.GetUserById(senderUserId);
        
        if(sender is null)
        {
            throw new UserNotFoundException();
        }
        
        if(sender.IsAdmin is false)
        {
            throw new UserNotAdministratorException();
        }
        
        var userWithSameEmail = await userInventory.GetUserByEmail(email);
        
        if(userWithSameEmail is not null)
        {
            throw new UserAlreadyInvitedException();
        }

        await userInventory.CreateUser(sender, email);
        var newUser = await userInventory.GetUserByEmail(email);
        if(newUser is null) throw new UserNotFoundException();

        var randoToken = Guid.NewGuid().ToString();
        var invitationToken = new InvitationToken
        {
            Token = randoToken,
            InvitedByUserId = senderUserId,
            InvitedUserId = newUser.Id,
        };
        var invitationTokenResult =  invitationTokenInventory.Add(invitationToken);
        
        newUser.InvitedByTokenId = invitationTokenResult.Id;
        await userInventory.UpdateUser(newUser);

        // TODO : We should send the email from there too :)
    }

}