
using Application.Auth.UsersAccountsServices.PasswordServices.Exceptions;
using Org.BouncyCastle.Security;

namespace AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;

public class PasswordChangeDto
{

    public required int UserId { get; set; }
    public required string CurrentPwd { get; set; }
    public required string Password { get; set; }

}

public static class PwdExtends
{
    public static void PasswordSimpleValidate(this PasswordChangeDto pwdChange)
    {
        if (string.IsNullOrWhiteSpace(pwdChange.Password) || string.IsNullOrWhiteSpace(pwdChange.CurrentPwd))
            throw new PasswordException(PasswordServicesMessagesException.ChangePassword);
    }
}