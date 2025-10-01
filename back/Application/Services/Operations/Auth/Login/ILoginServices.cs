using System.Security.Claims;
using Application.Services.Operations.Auth.Account.dtos;
using Domain.Entities.Authentication;


namespace Application.Services.Operations.Auth.Login;

public interface ILoginServices
{
    Task<ApiResponse<UserToken>> LoginAsync(LoginModelDto user);
    Task<ApiResponse<string>> LogoutAsync();
    Task<DateTime> GetLastLogin(string email);
    Task<ClaimsPrincipal> BuildUserClaims(UserAccount userAccount);
    Task<UserAccount> GetUser(string email);

}