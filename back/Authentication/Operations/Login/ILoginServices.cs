using Authentication.Entities;


namespace Authentication.Operations.Login;

public interface ILoginServices
{
    Task<UserToken> LoginAsync(LoginModel user);
    
}