
using Authentication.Entities;

namespace Authentication.Operations.Account;

public interface IAccountManagerServices
{
    Task<bool> IsUserExistCheckByEmail(string email);
    Task<bool> ConfirmEmailAddress(ConfirmEmail confirmEmail);
    Task<bool> ForgotPassword(ForgotPassword forgotPassword);
}
