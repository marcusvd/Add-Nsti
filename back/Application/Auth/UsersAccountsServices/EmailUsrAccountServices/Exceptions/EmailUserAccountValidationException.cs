namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Exceptions;

public class EmailUserAccountValidationException : EmailUserAccountException
{
    public EmailUserAccountValidationException(string message) : base(message) { }
}