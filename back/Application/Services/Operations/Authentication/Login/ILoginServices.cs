using System.Threading.Tasks;
using Authentication;
using Application.Services.Operations.Authentication.Dtos;


namespace Application.Services.Operations.Authentication.Login;

public interface ILoginServices
{
    Task<UserToken> Login(UserAccountDto user);
}