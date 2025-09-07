
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;

namespace Application.Services.Operations.Auth.Register;

public interface IRegisterUserAccountServices
{
    Task<UserToken> AddUserExistingCompanyAsync(AddUserExistingCompanyDto user, int companyId);
}