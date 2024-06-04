using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;

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

    public string GenerateRequestFromUserToAdminContent(Request request)
    {
        return $@"
        <h1>Nouvelle demande de review !</h1>
        <p>L'utilisateur {request.User.Email} a soumis une demande de test.</p>
        <p><strong>Produit:</strong> <a href='{request.Product.URL}'>{request.Product.Name}</a></p>
        <p><strong>Message:</strong> {request.UserMessage}</p>
        <p>Connectez-vous à l'application pour consulter la demande.</p>
        <p>Force à vous,</p>
        <p>Livl Corporation</p>
        ";
    }
}