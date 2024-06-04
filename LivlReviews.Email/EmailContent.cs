using LivlReviews.Domain.Domain_interfaces_output;

namespace LivlReviews.Email;

public class EmailContent : INotificationContent
{
    public string GenerateAccountInvitationTokenContent(string activationLink)
    {
        return $@"
            <h1>Bienvenue sur LivlReviews!</h1>
            <p>Pour confirmer la création de votre compte, cliquez sur le lien ci-dessous afin de définir votre mot de passe.</p>
            <a href='{activationLink}'>Activer mon compte</a>
            <p>Force à vous,</p>
            <p>Livl Corporation</p>
        ";
    }
}