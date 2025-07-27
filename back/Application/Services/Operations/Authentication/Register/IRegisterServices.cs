using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;
using Authentication;

namespace Application.Services.Operations.Authentication.Register
{
    public interface IRegisterServices
    {
        Task<UserToken> RegisterAsync(RegisterDto user);
    }
}