using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Authentication.Register
{
    public interface IRegisterServices
    {
         Task<UserToken> Register(MyUserDto user);
    }
}