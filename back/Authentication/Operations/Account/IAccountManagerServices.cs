
using Authentication.Entities;

namespace Authentication.Operations.Account;

public interface IAccountManagerServices
{
    Task<bool> IsUserExistCheckByEmailAsync(string email);
    Task<bool> ConfirmEmailAddressAsync(ConfirmEmail confirmEmail);
    Task<bool> ForgotPasswordAsync(ForgotPassword forgotPassword);
    Task<bool> ResetPasswordAsync(ResetPassword resetPassword);
}
