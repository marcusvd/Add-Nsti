
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Companies.Dtos;
using Domain.Entities.Authentication;


namespace Application.Services.Operations.Auth.CompanyAuthServices;

public interface ICompanyAuthServices
{
    Task<CompanyAuthDto> GetCompanyAuthAsync(int id);
    Task<CompanyProfileDto> GetCompanyProfileFullAsync(string companyAuthId);
    Task UpdateCompanyAuth(CompanyAuthDto companyAuth);
}
