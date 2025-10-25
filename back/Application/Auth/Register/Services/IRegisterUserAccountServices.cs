
using Domain.Entities.Authentication;
using  Application.Shared.Dtos;
using Application.Auth.Dtos;
using Application.Auth.Register.Dtos;

namespace Application.Auth.Register.Services;

public interface IRegisterUserAccountServices
{
    Task<UserToken> AddUserExistingCompanyAsync(AddUserExistingCompanyDto user, int companyId);
}