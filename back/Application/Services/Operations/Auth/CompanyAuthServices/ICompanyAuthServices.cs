
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Companies.Dtos;
using Domain.Entities.Authentication;


namespace Application.Services.Operations.Auth.CompanyAuthServices;

public interface ICompanyAuthServices
{
    Task<CompanyAuthDto> GetCompanyAuthAsync(int id);
    Task<CompanyAuthDto> GetCompanyAuthFullAsync(int id);
    Task<CompanyProfileDto> GetCompanyProfileFullAsync(string companyAuthId);
    Task<List<UserAccountDto>> GetUsersByCompanyIdAsync(int companyAuthId);
    Task<UserAuthProfileDto> GetUserByIdFullAsync(int id);
    Task UpdateCompanyAuth(CompanyAuthDto companyAuth);
}
