using Domain.Entities.Authentication;
using Application.Exceptions;
using Application.Helpers.Inject;
using UnitOfWork.Persistence.Operations;
using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;
using Application.BusinessesServices.Services.Auth;
using Domain.Entities.System.Companies;
using Domain.Entities.System.Businesses;
using Application.BusinessesServices.Services.Profile;


namespace Application.BusinessesServices.Services.Gateway;

public partial class BusinessServices : IBusinessServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;
    private readonly IBusinessAuthServices _businessAuthServices;
    private readonly IBusinessProfileServices _businessProfileServices;

    public BusinessServices(
                    IUnitOfWork genericRepo,
                    IValidatorsInject validatorsInject,
                    IBusinessAuthServices businessAuthServices,
                    IBusinessProfileServices businessProfileServices
      )
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
        _businessAuthServices = businessAuthServices;
        _businessProfileServices = businessProfileServices;
    }

    public async Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto dto, int id)
    {
        _validatorsInject.GenericValidators.Validate(dto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);


        var businessAuth = await _businessAuthServices.GetBusinessAsync(id);

        _validatorsInject.GenericValidators.Validate(dto.BusinessProfileId, businessAuth.BusinessProfileId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        businessAuth.Companies.Add(dto.Company);

        businessAuth.Companies.ToList()[0].CNPJ = dto.CNPJ;

        var businessProfile = await _businessProfileServices.GetByBusinessProfileIdAsync(businessAuth.BusinessProfileId);

        var companyProfile = new CompanyProfile()
        {
            CNPJ = dto.CNPJ,
            Address = dto.Address,
            Contact = dto.Contact
        };

        businessAuth.Companies.ToList()[0].CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = businessAuth.Companies.ToList()[0], UserAccountId = 1 });

        businessProfile.Companies.Add(companyProfile);

        return await UpdateSave(businessAuth, businessProfile);
    }

   private async Task<bool> UpdateSave(BusinessAuth businessAuth, BusinessProfile businessProfile)
    {
        _genericRepo.BusinessesAuth.Update(businessAuth);
        _genericRepo.BusinessesProfiles.Update(businessProfile);

        if (await _genericRepo.SaveID() && await _genericRepo.Save())
            return true;

        return false;
    }
 
  

}
