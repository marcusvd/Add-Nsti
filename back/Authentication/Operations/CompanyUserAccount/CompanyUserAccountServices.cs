using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication.Helpers;
using Authentication.Entities;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net;
using Authentication.Context;


namespace Authentication.Operations.Register;

public class CompanyUserAccountServices : ICompanyUserAccountServices
{
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly IdImDbContext _dbContext;
    // private readonly AuthGenericValidatorServices _genericValidatorServices;

    // private readonly EmailServer _emailService;
    // private readonly JwtHandler _jwtHandler;
    // private readonly IUrlHelper _url;
    public CompanyUserAccountServices(
          IdImDbContext dbContext,
          ILogger<AuthGenericValidatorServices> logger
          //   EmailServer emailService,
        //   JwtHandler jwtHandler,
        //   IUrlHelper url,

        //   AuthGenericValidatorServices genericValidatorServices,
      ) 
    {
        _dbContext = dbContext;
        // _emailService = emailService;
        // _jwtHandler = jwtHandler;
        // _url = url;
        // _genericValidatorServices = genericValidatorServices;
        _logger = logger;
    }

    // public async Task<UserToken> RegisterAsync(RegisterModel user)
    // {
    //     // var cUser = _dbContext.CompaniesUsersAccounts.fin
    // }

}