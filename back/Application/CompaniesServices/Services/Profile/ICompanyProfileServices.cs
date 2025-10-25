using Domain.Entities.System.Companies;
using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;
using Application.CompaniesServices.Dtos.Profile;

namespace Application.CompaniesServices.Services.Profile;

public interface ICompanyProfileServices
{

    CompanyProfile CompanyProfileEntityBuilder(PushCompanyDto dto);
    Task<CompanyProfileDto> GetCompanyProfileFullAsync(string cnpj);
    Task UpdateCompanyProfileSimple(CompanyProfileDto CompanyProfile);
    Task<CompanyProfile> GetProfileById(int id);

}
