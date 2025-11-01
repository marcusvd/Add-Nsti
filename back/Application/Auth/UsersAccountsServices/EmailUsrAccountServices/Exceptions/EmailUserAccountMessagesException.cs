using System.Reflection.Metadata;

namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Exceptions;

public static class EmailUserAccountMessagesException
{
    public static readonly string confirmEmail = $@"Error when trying to confirm email address: the confirmation link is invalid or expired.:";
    public static readonly string tokenGenerate = $@"Error when trying to generate token or send email:";
    public static readonly string emailChange = $@"Error when trying to change user email:";
    public static readonly string EmailIsNotConfirmed = "1.0|Email precisa ser confirmado! Acesse seus emails, caso não o encontre na caixa de entrada olhe na caixa de spam. Obrigado!";
    public static readonly string UserIsLocked = "1.11|Usuário está bloqueado.";
    

}