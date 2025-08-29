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
// public class AuthAdmServices : CommonObjectMapper, IAuthAdmServices
{
    private UserManager<UserAccount> _userManager;
    private readonly IBusinessAuthRepository _businessAuthRepository;
    private readonly ICompanyProfileAddService _companyProfileAddService;
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly JwtHandler _jwtHandler;
    // private readonly IMapper<Address, AddressDto> _addressMapper;
    // private readonly IMapper<Contact, ContactDto> _contactMapper;

    private readonly ICommonObjectMapper _mapper;


    public AuthAdmServices(
          UserManager<UserAccount> userManager,
          ILogger<AuthGenericValidatorServices> logger,
          IBusinessAuthRepository businessAuthRepository,
          ICompanyProfileAddService companyProfileAddService,
          JwtHandler jwtHandler,
          AuthGenericValidatorServices genericValidatorServices,
              ICommonObjectMapper mapper
      // IMapper<Address, AddressDto> addressMapper,
      // IMapper<Contact, ContactDto> contactMapper

      )
    //   ) : base(addressMapper, contactMapper)
    {
        _userManager = userManager;
        _logger = logger;
        _jwtHandler = jwtHandler;
        _businessAuthRepository = businessAuthRepository;
        _companyProfileAddService = companyProfileAddService;
        _genericValidatorServices = genericValidatorServices;
        _mapper = mapper;
        // _addressMapper = addressMapper;
        // _contactMapper = contactMapper;

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


        // return await _businessAuthRepository.GetBusinessFull(id);
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

        // var businessAuthToDb = new BusinessAuth() { Id = 0, Name = "test", BusinessProfileId = "test" };
        businessAuth.Companies.Add(_mapper.Map<CompanyAuthDto, CompanyAuth>(businessAuthUpdateDto.Company ?? new CompanyAuthDto() { Name = "invalid", TradeName = "invalid", CompanyProfileId = "invalid" }));

        // var businessAuthToDb = _mapper.BusinessAuthMapperUpdate(businessAuth, businessAuthUpdateDto);

        businessAuth.Companies.ToList()[0].CompanyProfileId = CompanyProfileIdAuthId;

        CompanyProfileDto companyProfile = new()
        {
            CompanyAuthId = businessAuth.Companies.ToList()[0].CompanyProfileId,
            Address = businessAuthUpdateDto.Address,
            Contact = businessAuthUpdateDto.Contact
        };


        _businessAuthRepository.Update(businessAuth);
        // _businessAuthRepository.Update(businessAuthToDb);

        if (await _companyProfileAddService.AddAsync(companyProfile))
            return await _businessAuthRepository.SaveAsync();

        return false;
    }
    // public bool Validate<T>(T dtoId, T paramId)
    // {
    //     if (!Equals(dtoId, paramId)) throw new AuthServicesException(GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);
    //     else
    //         return true;
    // }
    // public bool ValidateIdsString(int dtoId, int paramId)
    // {
    //     if (dtoId != paramId) throw new AuthServicesException(GlobalErrorsMessagesException.EntityFromIdIsNull);
    //     else
    //         return true;
    // }


}