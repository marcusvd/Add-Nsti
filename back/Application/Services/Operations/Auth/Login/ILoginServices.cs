using Domain.Entities.Authentication;


namespace Application.Services.Operations.Auth.Login;

public interface ILoginServices
{
    Task<UserToken> LoginAsync(LoginModel user);
    
}