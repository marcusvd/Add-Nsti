using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Authentication.Login
{
    public interface ILoginServices
    {
         Task<UserToken> Login(MyUserDto user);
    }
}