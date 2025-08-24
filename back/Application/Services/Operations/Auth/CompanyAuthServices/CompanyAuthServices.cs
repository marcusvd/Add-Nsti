using Authentication.Helpers;
using Domain.Entities.Authentication;
using Microsoft.Extensions.Logging;
using Authentication.AuthenticationRepository.BusinessRepository;

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

    public Task<bool> AddAsync(CompanyAuth entity)
    {



        return null;


        // _companyAuthRepository
    }
}
