using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;

namespace Application.Services.Operations.Authentication
{
    public interface IAccountServices
    {
        Task<MyUserDto> GetUserByIdAsync(int id);
        Task<MyUserDto> GetUserByNameAsync(string name);
        Task<MyUserDto> GetUserByNameAllIncludedAsync(string name);
        Task<List<MyUserDto>> GetAllUsersAsync();
        // Task<MyUserDto> UpdateUserAsync(MyUserDto user);
    }
}