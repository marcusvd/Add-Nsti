
using Application.CompaniesServices.Dtos;
using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;

namespace Application.CompaniesServices.Services.Gateway;

public interface ICompanyServices
{
    Task<bool> UpdateCompany_Auth_Profile(Update_Auth_ProfileDto update_Auth_Profile);
    Task<bool> AddCompanyAsync(PushCompanyDto pushCompany, int businessId);
}
