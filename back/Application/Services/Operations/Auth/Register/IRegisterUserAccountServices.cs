
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Register;

public interface IRegisterUserAccountServices
{
    Task<UserToken> AddUserExistingCompanyAsync(AddUserExistingCompanyDto user, int companyId);
}