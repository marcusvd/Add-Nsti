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

namespace Authentication.Operations.AuthAdm;

public class AuthAdmServices : IAuthAdmServices
// public class AuthAdmServices : ObjectMapper, IAuthAdmServices
{
    private UserManager<UserAccount> _userManager;
    private readonly IBusinessAuthRepository _businessAuthRepository;
    private readonly ICompanyProfileAddService _companyProfileAddService;
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly JwtHandler _jwtHandler;

    private readonly IObjectMapper _mapper;

    public AuthAdmServices(
          UserManager<UserAccount> userManager,
          ILogger<AuthGenericValidatorServices> logger,
          IBusinessAuthRepository businessAuthRepository,
          ICompanyProfileAddService companyProfileAddService,
          JwtHandler jwtHandler,
          AuthGenericValidatorServices genericValidatorServices,
          IObjectMapper mapper
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


        var businessGrouppDto = _mapper.Map<BusinessAuth, BusinessAuthDto>(businessGroup);


        if (businessGroup == null)
            return new BusinessAuthDto
            {
                Id = -1,
                Name = "Invalid",
                BusinessProfileId = "-1"
            };


        return businessGrouppDto;

    }
    public async Task<BusinessAuth> GetBusinessAsync(int id)
    {
        var businessGroup = await _businessAuthRepository.GetByPredicate(
              x => x.Id == id,
              null,
              selector => selector,
              ordeBy => ordeBy.OrderBy(x => x.Name)
              );

        if (businessGroup == null)
            return new BusinessAuth
            {
                Id = -1,
                Name = "Invalid",
                BusinessProfileId = "-1"
            };

        return businessGroup;
    }
    public async Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto businessAuthUpdateDto, int id)
    {

        _genericValidatorServices.Validate(businessAuthUpdateDto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        string CompanyProfileIdAuthId = Guid.NewGuid().ToString();

        var businessAuth = await _businessAuthRepository.GetByPredicate(
            x => x.Id == id && x.Deleted == DateTime.MinValue,
            null,
            selector => selector,
            null);

        _genericValidatorServices.Validate(businessAuthUpdateDto.BusinessProfileId, businessAuth.BusinessProfileId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        businessAuth.Companies.Add(_mapper.Map<CompanyAuthDto, CompanyAuth>(businessAuthUpdateDto.Company ?? new CompanyAuthDto() { Name = "invalid", TradeName = "invalid", CompanyProfileId = "invalid" }));
        
        businessAuth.Companies.ToList()[0].CompanyProfileId = CompanyProfileIdAuthId;

        CompanyProfileDto companyProfile = new()
        {
            CompanyAuthId = businessAuth.Companies.ToList()[0].CompanyProfileId,
            CNPJ = businessAuthUpdateDto.CNPJ,
            Address = businessAuthUpdateDto.Address,
            Contact = businessAuthUpdateDto.Contact
        };


        _businessAuthRepository.Update(businessAuth);

        if (await _companyProfileAddService.AddAsync(companyProfile))
            return await _businessAuthRepository.SaveAsync();

        return false;
    }

}