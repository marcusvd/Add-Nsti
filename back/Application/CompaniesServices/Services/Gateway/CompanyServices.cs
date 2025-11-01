using Microsoft.EntityFrameworkCore;

using Domain.Entities.Authentication;
using Application.Exceptions;
using Application.CompaniesServices.Dtos;
using Application.Helpers.Inject;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.System.Companies;
using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;
using Application.CompaniesServices.Dtos.Auth;
using Application.CompaniesServices.Services.Auth;
using Application.CompaniesServices.Services.Profile;
using Application.BusinessesServices.Services.Auth;

namespace Application.CompaniesServices.Services.Gateway;

public class CompanyServices : ICompanyServices
{
    private readonly IUnitOfWork _genericRepo;
    // private readonly IAuthServicesInjection _authServices;
    private readonly ICompanyAuthServices _companyAuthServices;
    private readonly ICompanyProfileServices _companyProfileServices;
    private readonly IBusinessAuthServices _businessAuthServices;
    private readonly IValidatorsInject _validatorsInject;

    public CompanyServices(
                            IUnitOfWork genericRepo,
                            // IAuthServicesInjection authServices,
                            ICompanyAuthServices companyAuthServices,
                            ICompanyProfileServices companyProfileServices,
                            IValidatorsInject validatorsInject,
                            IBusinessAuthServices businessAuthServices
                            )
    {
        _genericRepo = genericRepo;
        // _authServices = authServices;
        _companyAuthServices = companyAuthServices;
        _companyProfileServices = companyProfileServices;
        _validatorsInject = validatorsInject;
        _businessAuthServices = businessAuthServices;
    }

    public async Task<bool> AddCompanyAsync(PushCompanyDto pushCompany, int businessId)
    {
        _validatorsInject.GenericValidators.Validate(pushCompany.BusinessId, businessId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        var userAccountList = await GetUsersByBusinessId(businessId);

        linkCompanyAuthWithUserAccount(userAccountList, pushCompany);

        var businessProfile = _businessAuthServices.GetByBusinessAuthIdAsync(pushCompany.BusinessProfileId);

        MakeCompanyProfileEntity(pushCompany, businessProfile.Id);


        return await _genericRepo.Save() && await _genericRepo.SaveID();
    }
    private async Task<List<UserAccount>> GetUsersByBusinessId(int businessId)
    {
        return await _genericRepo.UsersAccounts.Get(
                x => x.BusinessAuthId == businessId && x.Deleted == DateTime.MinValue,
                add => add.Include(x => x.UserRoles)
                .ThenInclude(x => x.Role),
                selector => selector,
                null).ToListAsync();
    }

    private void MakeCompanyProfileEntity(PushCompanyDto pushCompany, int businessProfileId)
    {
        var companyProfile = _companyProfileServices.CompanyProfileEntityBuilder(pushCompany);
        companyProfile.BusinessProfileId = businessProfileId;
        _genericRepo.CompaniesProfile.Add(companyProfile);

    }
    private void linkCompanyAuthWithUserAccount(List<UserAccount> usersByBusinessId, PushCompanyDto pushCompany)
    {
        var userRoles = usersByBusinessId.SelectMany(x => x.UserRoles).ToList();

        var role = userRoles.FirstOrDefault(x => x.Role.Name == pushCompany.Role);

        var companyAuth = BuilderCompanyAuth(pushCompany.Company, role.UserId);

        _genericRepo.CompaniesAuth.Add(companyAuth);
    }

    private CompanyAuth BuilderCompanyAuth(CompanyAuthDto dto, int userId)
    {
        var joinCompanyUser = dto;
        joinCompanyUser.CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = dto, UserAccountId = userId });
        return joinCompanyUser;
    }


    public async Task<bool> UpdateCompany_Auth_Profile(Update_Auth_ProfileDto update_Auth_Profile)
    {
        var companyAuth = update_Auth_Profile.CompanyAuth;
        var companyProfile = update_Auth_Profile.CompanyProfile;

        await _companyAuthServices.UpdateCompanyAuthSimple(companyAuth);
        await _companyProfileServices.UpdateCompanyProfileSimple(companyProfile);

        return await _genericRepo.Save() && await _genericRepo.SaveID();
    }

}
