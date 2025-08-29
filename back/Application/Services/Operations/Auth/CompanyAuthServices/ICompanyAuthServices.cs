
using Domain.Entities.Authentication;


namespace Application.Services.Operations.Auth.CompanyAuthServices;

public interface ICompanyAuthServices
{
    Task<CompanyAuth> GetCompanyAuthAsync(int id);
    Task UpdateCompanyAuth(CompanyAuth companyAuth);
}
