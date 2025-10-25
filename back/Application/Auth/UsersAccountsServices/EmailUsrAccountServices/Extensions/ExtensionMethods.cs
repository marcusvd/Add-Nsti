using Domain.Entities.Authentication;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Exceptions;
using Application.Shared.Validators;

namespace Application.EmailUsrAccountServices.Extensions;

public static class ExtensionMethods
{
    public static UserAccount InitiateEmailChange(this UserAccount userAccount, string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new EmailUserAccountException("Email cannot be empty");

        if (!GenericValidators.IsValidEmail(newEmail))
            throw new EmailUserAccountException("Invalid format");

        return UserAccountAssignValue(userAccount, newEmail);
    }

    private static UserAccount UserAccountAssignValue(UserAccount userAccount, string newEmail)
    {
        userAccount.Email = newEmail;
        userAccount.NormalizedEmail = newEmail.ToUpperInvariant();
        userAccount.EmailConfirmed = false;

        return userAccount;
    }


}