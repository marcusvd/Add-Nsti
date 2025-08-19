using Microsoft.AspNetCore.Identity;

using Authentication.Entities;
using Authentication.Helpers;
using Microsoft.Extensions.Logging;
using Authentication.AuthenticationRepository.BusinessRepository;

namespace Authentication.Operations.AuthAdm;

public class AuthAdmServices : IAuthAdmServices
{
    private UserManager<UserAccount> _userManager;
    private readonly IBusinessRepository _businessRepository;
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly JwtHandler _jwtHandler;
    
    public AuthAdmServices(
          UserManager<UserAccount> userManager,
          ILogger<AuthGenericValidatorServices> logger,
          IBusinessRepository businessRepository,
          JwtHandler jwtHandler,
          AuthGenericValidatorServices genericValidatorServices
      ) 
    {
        _userManager = userManager;
        _logger = logger;
        _jwtHandler = jwtHandler;
        _businessRepository = businessRepository;
        _genericValidatorServices = genericValidatorServices;
    }

    public async Task<Business> BusinessAsync(int id)
    {
        return await _businessRepository.GetBusinessFull(id);
        
    }
}