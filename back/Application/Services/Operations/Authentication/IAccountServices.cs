using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;

namespace Application.Services.Operations.Authentication
{
    public interface IAccountServices
    {
        Task<UserAccountDto> GetUserByIdAsync(int id);
        Task<UserAccountDto> GetUserByNameAsync(string name);
        Task<UserAccountDto> GetUserByNameAllIncludedAsync(string name);
        Task<List<UserAccountDto>> GetAllUsersAsync();
        // Task<UserAccountDto> UpdateUserAsync(UserAccountDto user);
    }
}