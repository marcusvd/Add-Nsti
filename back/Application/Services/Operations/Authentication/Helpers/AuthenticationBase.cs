using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Shared.Dtos.Mappers;
using Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Operations.Authentication
{
    public class AuthenticationBase
    {
        private UserManager<UserAccount> _userManager;
        public AuthenticationBase(UserManager<UserAccount> userManager)
        {
            _userManager = userManager;
        }
        private protected async Task<UserAccount> FindUserAsync(string userNameOrEmail)
        {
            return await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail);
        }

        private protected async Task<bool> IsAccountLockedOutAsync(UserAccount userAccount)
        {
            return await _userManager.IsLockedOutAsync(userAccount);
        }

        private protected async Task<bool> IsEmailConfirmedAsync(UserAccount userAccount)
        {
            return await _userManager.IsEmailConfirmedAsync(userAccount);
        }

        private protected async Task<bool> IsPasswordValidAsync(UserAccount userAccount, string password)
        {
            var isValid = await _userManager.CheckPasswordAsync(userAccount, password);

            if (isValid)
            {
                await _userManager.ResetAccessFailedCountAsync(userAccount);
                return true;
            }
            else
            {
                await _userManager.AccessFailedAsync(userAccount);
                return false;
            }
        }

        private protected async Task<List<Claim>> BuildUserClaims(UserAccount userAccount)
        {
            var getRoles = await _userManager.GetRolesAsync(userAccount);

            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
              new Claim(ClaimTypes.Name, userAccount.UserName),
            };

            foreach (var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

    }
}