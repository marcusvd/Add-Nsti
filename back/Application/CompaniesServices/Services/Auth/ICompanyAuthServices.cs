using Application.CompaniesServices.Dtos.Auth;
using Application.Shared.Dtos;
using Domain.Entities.System.Companies;

namespace Application.CompaniesServices.Services.Auth;

public interface ICompanyAuthServices
{
    Task<CompanyAuth> GetAuthById(int id);
    Task<CompanyAuthDto> GetCompanyAuthAsync(int id);
    Task<CompanyAuthDto> GetCompanyAuthFullAsync(int id);
    Task<List<CompanyAuthDto>> GetCompaniesByUserIdAsync(int userId);
    Task<ApiResponse<CompaniesQtsViewModel>> GetAmountCompaniesByUserIdAsync(int userId);
    Task UpdateCompanyAuthSimple(CompanyAuthDto companyAuth);
}
