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

        public async Task<MyUserDto> GetUserByNameAsync(string name)
        {
            var myUserFromDb = await _iAuthHelpersServices.FindUserByNameAsync(name);

            var myUserDtoReturn = _mapper.MyUserMapper(myUserFromDb);

            return myUserDtoReturn;
        }
        public async Task<MyUserDto> GetUserByNameAllIncludedAsync(string name)
        {
            var myUserFromDb = await _iAuthHelpersServices.FindUserByNameAllIncludedAsync(name);

            var myUserDtoReturn = _mapper.MyUserMapper(myUserFromDb);

            return myUserDtoReturn;
        }
        public async Task<MyUserDto> GetUserByIdAsync(int id)
        {
            var myUserFromDb = await _iAuthHelpersServices.FindUserByIdAsync(id);

            var myUserDtoReturn = _mapper.MyUserMapper(myUserFromDb);

            return myUserDtoReturn;
        }
        public async Task<List<MyUserDto>> GetAllUsersAsync()
        {
            var myUsersFromDb = await _iAuthHelpersServices.FindAllUsersAsync();
            var myUsersDtoReturn = _mapper.MyUserListMake(myUsersFromDb);
            return myUsersDtoReturn;
        }

        // public async Task<MyUserDto> UpdateUserAsync(MyUserDto user)
        // {

        //     var myUserFromDb = await _iAuthHelpersServices.FindUserByIdAsync(user.Id);

        //     var toUpdate = _iMapper.Map(user, myUserFromDb);

        //     if (myUserFromDb.NormalizedEmail != user.Email.ToUpper())
        //     {
        //         toUpdate.EmailConfirmed = false;
        //     }


        //     var result = await _iAuthHelpersServices.UserUpdateAsync(toUpdate);
        //     if (user.PasswordChanged)
        //     {

        //         var resetPwd = new ResetPasswordDto()
        //         {
        //             Password = user.Password,
        //             Email = myUserFromDb.Email,
        //             Token =  await _iAuthHelpersServices.GeneratePasswordResetTokenAsync(myUserFromDb)
        //         };

        //         await _iAuthHelpersServices.ResetPasswordAsync(resetPwd);
        //     }
            
            
        //     if (result.Succeeded)
        //     {
        //         var myUserUpdatedFropmDb = await _iAuthHelpersServices.FindUserByNameAsync(user.UserName);
        //         return _iMapper.Map<MyUserDto>(myUserUpdatedFropmDb);
        //     }

        //     return user;

        // }



    }
}