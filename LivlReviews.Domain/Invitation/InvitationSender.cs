using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using LivlReviews.Domain.Exceptions;
using LivlReviews.Domain.Models;

namespace LivlReviews.Domain.Invitation;

public class InvitationSender(
    IInvitationTokenInventory invitationTokenInventory, 
    IUserInventory userInventory, 
    INotificationManager notificationManager,
    string? frontendBaseUrl) : IInvitationSender
{

    public async Task SendInvitation(string senderUserId, string email)
    {
        IUser? sender = await userInventory.GetUserById(senderUserId);
        
        if(sender is null)
        {
            throw new UserNotFoundException();
        }
        
        if(!sender.IsAdmin)
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

        List<RecipientNotificationInvitation> recipientEmailInvitations =
        [
            new RecipientNotificationInvitation
            {
                Contact = newUser.Email,
                ActivationLink = $"{frontendBaseUrl}/auth/password?token={randoToken}"
            }
        ];
        await notificationManager.SendAccountInvitationNotification(recipientEmailInvitations);
    }

}