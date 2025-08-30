using Authentication.Helpers;
using Domain.Entities.Authentication;
using Microsoft.Extensions.Logging;
using Authentication.AuthenticationRepository.BusinessRepository;
using Application.Exceptions;
using Repository.Data.Operations.Companies;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Shared.Mappers.BaseMappers;
using Domain.Entities.System.BusinessesCompanies;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Operations.Auth.CompanyAuthServices;


public class CompanyAuthServices : ICompanyAuthServices
{
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly ICompanyAuthRepository _companyAuthRepository;
    private readonly ICompanyProfileRepository _companyProfileRepository;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly IObjectMapper _objectMapper;

    public CompanyAuthServices(
          AuthGenericValidatorServices genericValidatorServices,
          ILogger<AuthGenericValidatorServices> logger,
          ICompanyAuthRepository companyAuthRepository,
          ICompanyProfileRepository companyProfileRepository,
          IObjectMapper objectMapper
      )
    {

        _genericValidatorServices = genericValidatorServices;
        _companyAuthRepository = companyAuthRepository;
        _companyProfileRepository = companyProfileRepository;
        _objectMapper = objectMapper;
        _logger = logger;
    }

    public async Task<CompanyAuthDto> GetCompanyAuthAsync(int id)
    {
        var result = await _companyAuthRepository.GetByPredicate(
            x => x.Id == id && x.Deleted == DateTime.MinValue,
            null,
            selector => selector,
            null
            );

        if (result == null) throw new Exception(GlobalErrorsMessagesException.IsObjNull);

        return _objectMapper.Map<CompanyAuth, CompanyAuthDto>(result);
    }

    public async Task<CompanyProfileDto> GetCompanyProfileFullAsync(string companyAuthId)
    {
        var result = await _companyProfileRepository.GetByPredicate(
            x => x.CompanyAuthId == companyAuthId && x.Deleted == DateTime.MinValue,
            add => add.Include(x => x.Address)
            .Include(x => x.Contact),
            selector => selector,
            null
            );

        if (result == null) throw new Exception(GlobalErrorsMessagesException.IsObjNull);

        return _objectMapper.Map<CompanyProfile, CompanyProfileDto>(result);
    }

    public Task UpdateCompanyAuth(CompanyAuthDto companyAuth)
    {

        CompanyAuth update = _objectMapper.Map<CompanyAuthDto, CompanyAuth>(companyAuth);

        _companyAuthRepository.Update(update);

        return Task.CompletedTask;
    }




}
