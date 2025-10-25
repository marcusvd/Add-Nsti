namespace Application.Auth.UsersAccountsServices.Exceptions;

public static class UserAccountMessagesException
{

    public static readonly string EmailIsNotConfirmed = "1.0|Email precisa ser confirmado! Acesse seus emails, caso não o encontre na caixa de entrada olhe na caixa de spam. Obrigado!";
    public static readonly string UserIsLocked = "1.11|Usuário está bloqueado.";
}