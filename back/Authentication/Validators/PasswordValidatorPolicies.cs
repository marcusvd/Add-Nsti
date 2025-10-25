
using Microsoft.AspNetCore.Identity;


namespace Authentication.Validators;

public class PasswordValidatorPolicies<TUser> : IPasswordValidator<TUser> where TUser : class
{

    public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
    {

        if (string.IsNullOrEmpty(password))
        {
            return IdentityResult.Failed(

                new IdentityError { Description = "Password cannot be null or empty." }
            );
        }

        var userName = await manager.GetUserNameAsync(user);

        if (userName == password)
        {
            return IdentityResult.Failed(
                new IdentityError { Description = "Username cannot be the same as the password." }
            );
        }

        if (password.ToUpper().Contains("PASSWORD"))
        {
            return IdentityResult.Failed(
                new IdentityError { Description = "password cannot be the the word PASSWORD." }
            );
        }
        if (password.ToUpper().Contains("SENHA"))
        {
            return IdentityResult.Failed(
                new IdentityError { Description = "password cannot be the the word PASSWORD." }
            );
        }

        return IdentityResult.Success;
    }
}