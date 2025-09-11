using Domain.Entities.Authentication;
using Application.Exceptions;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Companies.Dtos;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Shared.Dtos;
using UnitOfWork.Persistence.Operations;

namespace Authentication.Operations.AuthAdm;

public class AuthAdmServices : IAuthAdmServices
{
    private readonly IUnitOfWork _GENERIC_REPO;

    public AuthAdmServices(
                    IUnitOfWork GENERIC_REPO
      )
    {
        // _genericValidatorServices = genericValidatorServices;
        _GENERIC_REPO = GENERIC_REPO;
    }

    public async Task<BusinessAuthDto> GetBusinessFullAsync(int id)
    {

        var businessGroup = await _GENERIC_REPO.BusinessesAuth.GetByPredicate(
                x => x.Id == id,
                add =>
                add.Include(x => x.UsersAccounts)
               .Include(x => x.Companies)
               .ThenInclude(x => x.CompanyUserAccounts),
                selector => selector,
                ordeBy => ordeBy.OrderBy(x => x.Name)
                );

        return businessGroup.ToDto();

    }
    public async Task<BusinessAuthDto> GetBusinessAsync(int id)
    {
        var businessGroup = await _GENERIC_REPO.BusinessesAuth.GetByPredicate(
              x => x.Id == id,
              null,
              selector => selector,
              ordeBy => ordeBy.OrderBy(x => x.Name)
              );

        if (businessGroup == null)
            return (BusinessAuthDto)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<BusinessAuthDto>();

        return businessGroup.ToDto();
    }
    public async Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto businessAuthUpdateDto, int id)
    {

        _GENERIC_REPO._GenericValidatorServices.Validate(businessAuthUpdateDto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        string CompanyProfileIdAuthId = Guid.NewGuid().ToString();

        var businessAuth = await GetBusinessAuthAsync(id);

        _GENERIC_REPO._GenericValidatorServices.Validate(businessAuthUpdateDto.BusinessProfileId, businessAuth.BusinessProfileId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        businessAuth.Companies.Add(businessAuthUpdateDto.Company.ToEntity() ?? (CompanyAuth)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<CompanyAuth>());

        businessAuth.Companies.ToList()[0].CompanyProfileId = CompanyProfileIdAuthId;

        var businessProfile = await GetBusinessProfileAsync(businessAuth.BusinessProfileId);

        var companyProfile = CompanyProfileEntityBuilder(businessAuthUpdateDto, CompanyProfileIdAuthId);

        businessProfile.Companies.Add(companyProfile);

        return await UpdateSave(businessAuth, businessProfile);
    }
    private async Task<BusinessAuth> GetBusinessAuthAsync(int id)
    {
        return await _GENERIC_REPO.BusinessesAuth.GetByPredicate(
                    x => x.Id == id && x.Deleted == DateTime.MinValue,
                    null,
                    selector => selector,
                    null);
    }
    private async Task<BusinessProfile> GetBusinessProfileAsync(string businessAuth)
    {
        return await _GENERIC_REPO.BusinessesProfiles.GetByPredicate(
            x => x.BusinessAuthId == businessAuth,
            null,
            selector => selector,
            null
            );
    }
    private CompanyProfile CompanyProfileEntityBuilder(BusinessAuthUpdateAddCompanyDto dto, string companyProfileIdAuthId)
    {
        return new()
        {
            CompanyAuthId = companyProfileIdAuthId,
            CNPJ = dto.CNPJ,
            Address = dto.Address.ToEntity(),
            Contact = dto.Contact.ToEntity()
        };
    }
    private async Task<bool> UpdateSave(BusinessAuth businessAuth, BusinessProfile businessProfile)
    {

        _GENERIC_REPO.BusinessesAuth.Update(businessAuth);
        _GENERIC_REPO.BusinessesProfiles.Update(businessProfile);

        if (await _GENERIC_REPO.SaveID() && await _GENERIC_REPO.Save())
            return true;

        return false;
    }

}
