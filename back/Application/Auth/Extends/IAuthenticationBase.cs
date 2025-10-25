using Application.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.Extends;

public interface IAuthenticationBase
{

        string IsValidEmail(string email);
        int ValidateUserId(int userId);
        // Task<UserAccount> GetUserAccountByEmail(string email);
        // Task<UserAccount> GetUserAccountByUserId(int id);



        // Task<UserAccount> FindUserAsync(string userNameOrEmail);
        // Task<UserAccount> FindUserByIdAsync(int id);
        // Task<IdentityResult> IsUserExist(string email);
        // Task<IdentityResult> CheckPasswordAsync(UserAccount userAccount, string password);
        // Task<IdentityResult> PasswordSignInAsync(UserAccount userAccount, string password, bool isPersistent = true, bool lockoutOnFailure = true);
        // public RoleDto CreateRole(string role, string DisplayRole);
        // Task<string> GenerateUrlTokenEmailChange(UserAccount userAccount, string action, string controller, string newEmail);
        // Task<string> GenerateUrlTokenPasswordReset(UserAccount userAccount, string action, string controller);
        // Task<string[]> UpdateUserRoles(UpdateUserRoleDto[] roles);
        // Task<IList<string>> GetRolesAsync(UserAccount userAccount);
        // Task<IdentityResult> CreateRoleAsync(RoleDto roleDto);
        // Task<IdentityResult> IsUserExistCheckByEmailAsync(string email);
        // Task<IdentityResult> RequestEmailChangeAsync(RequestEmailChangeDto updateUserAccountEmailDto);
        // Task SendEmailConfirmationAsync(DataConfirmEmail dataConfirmEmail, string body);
        // DataConfirmEmail DataConfirmEmailMaker(UserAccount user, string[] dataConfirmation);        Task<IdentityResult> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto confirmRequestEmailChange);
        // Task<IdentityResult> ConfirmEmailAddressAsync(ConfirmEmailDto confirmEmail);
        // Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordDto forgotPassword);
        // Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword);
        // Task<IdentityResult> PasswdChangeAsync(UserAccount user, string CurrentPwd, string NewPwd);
        // Task SendAsync(string To = "register@nostopti.com.br", string From = "register@nostopti.com.br",
        //             string Subject = "Test Subject", string Body = "Test", string MailServer = "smtp.nostopti.com.br",
        //              int Port = 587, bool IsUseSsl = false, string UserName = "register@nostopti.com.br", string Password = "Nsti$2024");

}