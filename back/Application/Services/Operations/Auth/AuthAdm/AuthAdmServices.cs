using Microsoft.AspNetCore.Identity;

using Domain.Entities.Authentication;
using Authentication.Helpers;
using Microsoft.Extensions.Logging;
using Authentication.Jwt;
using Authentication.AuthenticationRepository.BusinessAuthRepository;
using Authentication.Exceptions;
using Application.Exceptions;
using Application.Services.Shared.Dtos.Mappers;
using Application.Services.Operations.Auth.Dtos;
using Repository.Data.Operations.Companies;
using Application.Services.Operations.Companies;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Shared.Dtos;

namespace Authentication.Operations.AuthAdm;

public class AuthAdmServices : IAuthAdmServices
{
    private UserManager<UserAccount> _userManager;
    private readonly IBusinessAuthRepository _businessAuthRepository;
    private readonly ICompanyProfileAddService _companyProfileAddService;
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly JwtHandler _jwtHandler;
    private readonly ICommonObjectMapper _mapper;

    public AuthAdmServices(
          UserManager<UserAccount> userManager,
          ILogger<AuthGenericValidatorServices> logger,
          IBusinessAuthRepository businessAuthRepository,
          ICompanyProfileAddService companyProfileAddService,
          JwtHandler jwtHandler,
          AuthGenericValidatorServices genericValidatorServices,
          ICommonObjectMapper mapper
      )
    {
        _userManager = userManager;
        _logger = logger;
        _jwtHandler = jwtHandler;
        _businessAuthRepository = businessAuthRepository;
        _companyProfileAddService = companyProfileAddService;
        _genericValidatorServices = genericValidatorServices;
        _mapper = mapper;
    }

    public async Task<BusinessAuth> BusinessAsync(int id)
    {
        return await _businessAuthRepository.GetBusinessFull(id);
    }


    public async Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto businessAuthUpdateDto, int id)
    {

        Validate(businessAuthUpdateDto.Id, id);

        string CompanyProfileIdAuthId = Guid.NewGuid().ToString();

        var businessAuth = await _businessAuthRepository.GetByPredicate(
            x => x.Id == id && x.Deleted == DateTime.MinValue,
            null,
            selector => selector,
            null);

        Validate(businessAuthUpdateDto.BusinessProfileId, businessAuth.BusinessProfileId);

        var businessAuthToDb = _mapper.BusinessAuthMapperUpdate(businessAuth, businessAuthUpdateDto);

        businessAuthToDb.Companies.ToList()[0].CompanyProfileId = CompanyProfileIdAuthId;

        CompanyProfileDto companyProfile = new()
        {
            CompanyAuthId = businessAuthToDb.Companies.ToList()[0].CompanyProfileId,
            Address = businessAuthUpdateDto.Address,
            Contact = businessAuthUpdateDto.Contact
        };


        _businessAuthRepository.Update(businessAuthToDb);

        if (await _companyProfileAddService.AddAsync(companyProfile))
            return await _businessAuthRepository.SaveAsync();

        return false;
    }

    public bool Validate<T>(T dtoId, T paramId)
    {
        if (!Equals(dtoId, paramId)) throw new AuthServicesException(GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);
        else
            return true;
    }
    // public bool ValidateIdsString(int dtoId, int paramId)
    // {
    //     if (dtoId != paramId) throw new AuthServicesException(GlobalErrorsMessagesException.EntityFromIdIsNull);
    //     else
    //         return true;
    // }


}