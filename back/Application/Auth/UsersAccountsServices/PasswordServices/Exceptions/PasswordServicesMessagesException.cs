namespace Application.Auth.UsersAccountsServices.PasswordServices.Exceptions;

public static class PasswordServicesMessagesException
{
    public static readonly string PasswordWillExpire = "1.15|O a senha expirou, acesse o email e redefina sua senha para o desbloqueio de sua conta.";
    public static readonly string ResetPassword = "1.12|Erro durante redefinição de senha.";
    public static readonly string ForgotPassword = "Não foi possível alterar a senha. Verifique o e-mail e tente novamente.";
    public static readonly string ChangePassword = "Nova senha e senha atual não pode ser vazia.";
    
}