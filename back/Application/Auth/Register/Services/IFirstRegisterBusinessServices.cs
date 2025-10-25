using Application.Auth.Register.Dtos.FirstRegister;
using Domain.Entities.Authentication;

namespace Application.Auth.Register.Services;

public interface IFirstRegisterBusinessServices
{
        Task<UserToken> RegisterAsync(RegisterModelDto user);
}