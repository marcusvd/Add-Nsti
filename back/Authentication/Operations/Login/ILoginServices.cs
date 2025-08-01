using Authentication.Entities;


namespace Authentication.Operations.Login;

public interface ILoginServices
{
    Task<UserToken> Login(LoginModel user);
    
}