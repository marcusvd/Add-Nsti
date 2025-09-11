using Application.Services.Operations.Auth.Account.dtos;
using Domain.Entities.Authentication;


namespace Application.Services.Operations.Auth.Login;

public interface ILoginServices
{
    Task<UserToken> LoginAsync(LoginModelDto user);
    
}