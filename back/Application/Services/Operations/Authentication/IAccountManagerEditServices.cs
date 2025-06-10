using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;


namespace Application.Services.Operations.Authentication
{
    public interface IAccountManagerEditServices
    {
        Task<MyUserDto> GetUserByName(string userName);
        
    }
}