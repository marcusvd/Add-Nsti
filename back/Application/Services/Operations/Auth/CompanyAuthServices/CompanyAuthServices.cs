using Authentication.Helpers;
using Domain.Entities.Authentication;
using Microsoft.Extensions.Logging;
using Authentication.AuthenticationRepository.BusinessRepository;
using Application.Exceptions;

namespace Application.Services.Operations.Auth.CompanyAuthServices;


public class CompanyAuthServices : ICompanyAuthServices
{
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly ICompanyAuthRepository _companyAuthRepository;
    private readonly AuthGenericValidatorServices _genericValidatorServices;

    public CompanyAuthServices(
          AuthGenericValidatorServices genericValidatorServices,
          ILogger<AuthGenericValidatorServices> logger,
          ICompanyAuthRepository companyAuthRepository
      )
    {

        _genericValidatorServices = genericValidatorServices;
        _companyAuthRepository = companyAuthRepository;
        _logger = logger;
    }

    public async Task<CompanyAuth> GetCompanyAuthAsync(int id)
    {
        var result = await _companyAuthRepository.GetByPredicate(
            x => x.Id == id && x.Deleted == DateTime.MinValue,
            null,
            selector => selector,
            null
            );

        if (result == null) throw new Exception(GlobalErrorsMessagesException.IsObjNull);


        return result;
    }
    
    public Task UpdateCompanyAuth(CompanyAuth companyAuth)
    {
        _companyAuthRepository.Update(companyAuth);

        return Task.CompletedTask;
    }




}
