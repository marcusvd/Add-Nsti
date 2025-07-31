using Authentication.Dtos;
using Authentication.Entities;


namespace Authentication.Login;

public interface ILoginServices
{
    Task<UserToken> Login(LoginDto user);
    
}