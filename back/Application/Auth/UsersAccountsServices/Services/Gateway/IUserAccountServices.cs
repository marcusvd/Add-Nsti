
using Application.Auth.Dtos;
using Application.UsersAccountsServices.Dtos;


namespace Application.Auth.UsersAccountsServices.Services.Gateway;

public interface IUserAccountServices
{
    Task<List<UserAccountDto>> GetUsersByCompanyIdAsync(int companyAuthId);
    Task<UserAuthProfileDto> GetUserByIdFullAsync(int id);
}