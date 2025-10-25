using Application.Auth.Login.Dtos;
using Application.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.Login.Extends;


public interface ILoginServices
{
    Task<ApiResponse<UserToken>> LoginAsync(LoginModelDto user);
    Task<ApiResponse<string>> LogoutAsync();
    Task<DateTime> GetLastLogin(string email);
    // Task<UserAccount> GetUser(string email);

}