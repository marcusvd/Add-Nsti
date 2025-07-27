using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Helpers;

namespace Application.Services.Operations.Authentication
{

    public class AccountManagerEditServices : IAccountManagerEditServices
    {
        private readonly AuthHelpersServices _authHelpersServices;

        public AccountManagerEditServices(
        AuthHelpersServices authHelpersServices
        )
        {
             _authHelpersServices = authHelpersServices;;
        }
        public async Task<UserAccountDto> GetUserByName(string userName)
        {
            var userAccount = await _authHelpersServices.FindUserByNameAsync(userName);

            var userAccountDto = new UserAccountDto()
            {
                Id = userAccount.Id,
                UserName = userAccount.UserName,
                Email = userAccount.Email,
                TwoFactorEnabled = await _authHelpersServices.GetTwoFactorEnabledAsync(userAccount)
            };

            return userAccountDto;
        }
        public async Task<UserAccountDto> EditUserByName(string userName)
        {
            var userAccount = await _authHelpersServices.FindUserByNameAsync(userName);

            var userAccountDto = new UserAccountDto()
            {
                UserName = userAccount.UserName,
                Email = userAccount.Email,
                TwoFactorEnabled = await _authHelpersServices.GetTwoFactorEnabledAsync(userAccount)
            };

            return userAccountDto;
        }

    }
}