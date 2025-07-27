using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Shared.Dtos.Mappers;

namespace Application.Services.Operations.Authentication
{
    public class AccountServices : IAccountServices
    {

        private readonly IAuthHelpersServices _iAuthHelpersServices;
        private readonly ICommonObjectMapper _mapper;

        // private readonly JwtHandler _jwtHandler;
        // private readonly Email _email;
        public AccountServices(
        IAuthHelpersServices iAuthHelpersServices,
        ICommonObjectMapper mapper
        )
        {
            _iAuthHelpersServices = iAuthHelpersServices;
            _mapper = mapper;
        }

        public async Task<UserAccountDto> GetUserByNameAsync(string name)
        {
            var userAccountFromDb = await _iAuthHelpersServices.FindUserByNameAsync(name);

            var userAccountDtoReturn = _mapper.UserAccountMapper(userAccountFromDb);

            return userAccountDtoReturn;
        }
        public async Task<UserAccountDto> GetUserByNameAllIncludedAsync(string name)
        {
            var userAccountFromDb = await _iAuthHelpersServices.FindUserByNameAllIncludedAsync(name);

            var userAccountDtoReturn = _mapper.UserAccountMapper(userAccountFromDb);

            return userAccountDtoReturn;
        }
        public async Task<UserAccountDto> GetUserByIdAsync(int id)
        {
            var userAccountFromDb = await _iAuthHelpersServices.FindUserByIdAsync(id);

            var userAccountDtoReturn = _mapper.UserAccountMapper(userAccountFromDb);

            return userAccountDtoReturn;
        }
        public async Task<List<UserAccountDto>> GetAllUsersAsync()
        {
            var userAccountsFromDb = await _iAuthHelpersServices.FindAllUsersAsync();
            var userAccountsDtoReturn = _mapper.UserAccountListMake(userAccountsFromDb);
            return userAccountsDtoReturn;
        }

        // public async Task<UserAccountDto> UpdateUserAsync(UserAccountDto user)
        // {

        //     var userAccountFromDb = await _iAuthHelpersServices.FindUserByIdAsync(user.Id);

        //     var toUpdate = _iMapper.Map(user, userAccountFromDb);

        //     if (userAccountFromDb.NormalizedEmail != user.Email.ToUpper())
        //     {
        //         toUpdate.EmailConfirmed = false;
        //     }


        //     var result = await _iAuthHelpersServices.UserUpdateAsync(toUpdate);
        //     if (user.PasswordChanged)
        //     {

        //         var resetPwd = new ResetPasswordDto()
        //         {
        //             Password = user.Password,
        //             Email = userAccountFromDb.Email,
        //             Token =  await _iAuthHelpersServices.GeneratePasswordResetTokenAsync(userAccountFromDb)
        //         };

        //         await _iAuthHelpersServices.ResetPasswordAsync(resetPwd);
        //     }
            
            
        //     if (result.Succeeded)
        //     {
        //         var userAccountUpdatedFropmDb = await _iAuthHelpersServices.FindUserByNameAsync(user.UserName);
        //         return _iMapper.Map<UserAccountDto>(userAccountUpdatedFropmDb);
        //     }

        //     return user;

        // }



    }
}