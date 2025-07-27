using System.Collections.Generic;


using Application.Services.Operations.Inheritances.Enums;
using Application.Services.Shared.Dtos.Mappers;
using Authentication;
using Domain.Entities.Customers;

namespace Application.Services.Operations.Authentication.Dtos.Mappers;

public partial class AuthenticationObjectMapperServices : CommonObjectMapper, IAuthenticationObjectMapperServices
{

    public UserAccount RegisterMapper(RegisterDto entity)
    {
        if (entity == null) return null;

        var obj = new UserAccount()
        {
            UserName = entity.UserName,
            Email = entity.Email,
        };

        return obj;
    }
        public UserAccountDto UserAccountMapper(UserAccount entity)
        {
            var user = new UserAccountDto()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
            };
            return user;
        }
        public UserAccount UserAccountMapper(UserAccountDto entity)
        {
            var user = new UserAccount()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
            };
            return user;
        }
        public List<UserAccountDto> UserAccountListMake(List<UserAccount> list)
        {
            if (list == null) return null;

            var toReturn = new List<UserAccountDto>();

            list.ForEach(x =>
            {
                toReturn.Add(UserAccountMapper(x));
            });

            return toReturn;
        }
        public List<UserAccount> UserAccountListMake(List<UserAccountDto> list)
        {
            if (list == null) return null;

            var toReturn = new List<UserAccount>();

            list.ForEach(x =>
            {
                toReturn.Add(UserAccountMapper(x));
            });

            return toReturn;
        }
}
