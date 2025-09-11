
using Application.Services.Operations.Auth.Dtos;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Register;

public interface IFirstRegisterBusinessServices
{
    Task<UserToken> RegisterAsync(RegisterModelDto user);
}