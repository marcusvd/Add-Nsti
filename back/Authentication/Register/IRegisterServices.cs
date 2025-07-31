
using Authentication;
using Authentication.Dtos;
using Authentication.Entities;

namespace Authentication.Register
{
    public interface IRegisterServices
    {
        Task<UserToken> RegisterAsync(RegisterDto user);
    }
}