using Microsoft.AspNetCore.Identity;

using Domain.Entities.Authentication;
using Authentication.Helpers;
using Microsoft.Extensions.Logging;
using Authentication.Jwt;
using Authentication.AuthenticationRepository.BusinessAuthRepository;
using Application.Exceptions;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Companies;
using Application.Services.Operations.Companies.Dtos;
using Microsoft.EntityFrameworkCore;
using Application.Services.Shared.Mappers.BaseMappers;
using Repository.Data.Operations.BusinessesProfiles;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Shared.Dtos;
using UnitOfWork.Persistence.Operations;

namespace Authentication.Operations.AuthAdm;

public class AuthAdmServices : IAuthAdmServices
// public class AuthAdmServices : ObjectMapper, IAuthAdmServices
{
    private UserManager<UserAccount> _userManager;
    private readonly IBusinessAuthRepository _businessAuthRepository;
    private readonly IBusinessesProfilesRepository _businessesProfilesRepository;
    private readonly ICompanyProfileAddService _companyProfileAddService;
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly JwtHandler _jwtHandler;
    private readonly IObjectMapper _mapper;
    private readonly IUnitOfWork _GENERIC_REPO;

    public AuthAdmServices(
          UserManager<UserAccount> userManager,
          ILogger<AuthGenericValidatorServices> logger,
          IBusinessAuthRepository businessAuthRepository,
          IBusinessesProfilesRepository businessesProfilesRepository,
          ICompanyProfileAddService companyProfileAddService,
          JwtHandler jwtHandler,
          AuthGenericValidatorServices genericValidatorServices,
          IObjectMapper mapper,
          IUnitOfWork GENERIC_REPO
      )
    {
        _userManager = userManager;
        _logger = logger;
        _jwtHandler = jwtHandler;
        _businessAuthRepository = businessAuthRepository;
        _businessesProfilesRepository = businessesProfilesRepository;
        _companyProfileAddService = companyProfileAddService;
        _genericValidatorServices = genericValidatorServices;
        _mapper = mapper;
        _GENERIC_REPO = GENERIC_REPO;
    }

    public async Task<BusinessAuthDto> GetBusinessFullAsync(int id)
    {

        var businessGroup = await _businessAuthRepository.GetByPredicate(
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
        var businessGroup = await _businessAuthRepository.GetByPredicate(
              x => x.Id == id,
              null,
              selector => selector,
              ordeBy => ordeBy.OrderBy(x => x.Name)
              );

        if (businessGroup == null)
            return (BusinessAuthDto)_genericValidatorServices.ReplaceNullObj<BusinessAuthDto>();

        return businessGroup.ToDto();
    }
    public async Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto businessAuthUpdateDto, int id)
    {

        _genericValidatorServices.Validate(businessAuthUpdateDto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        string CompanyProfileIdAuthId = Guid.NewGuid().ToString();

        var businessAuth = await GetBusinessAuthAsync(id);

        _genericValidatorServices.Validate(businessAuthUpdateDto.BusinessProfileId, businessAuth.BusinessProfileId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        businessAuth.Companies.Add(businessAuthUpdateDto.Company.ToEntity() ?? (CompanyAuth)_genericValidatorServices.ReplaceNullObj<CompanyAuth>());

        businessAuth.Companies.ToList()[0].CompanyProfileId = CompanyProfileIdAuthId;

        var businessProfile = await GetBusinessProfileAsync(businessAuth.BusinessProfileId);

        var companyProfile = CompanyProfileEntityBuilder(businessAuthUpdateDto, CompanyProfileIdAuthId);

        businessProfile.Companies.Add(companyProfile);

        return await UpdateSave(businessAuth, businessProfile);
    }

    private async Task<BusinessAuth> GetBusinessAuthAsync(int id)
    {
        return await _businessAuthRepository.GetByPredicate(
                    x => x.Id == id && x.Deleted == DateTime.MinValue,
                    null,
                    selector => selector,
                    null);
    }

    private async Task<BusinessProfile> GetBusinessProfileAsync(string businessAuth)
    {
        return await _businessesProfilesRepository.GetByPredicate(
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

        _businessAuthRepository.Update(businessAuth);
        _businessesProfilesRepository.Update(businessProfile);

        if (await _businessAuthRepository.SaveAsync() && await _GENERIC_REPO.save())
            return true;

        return false;
    }



}
