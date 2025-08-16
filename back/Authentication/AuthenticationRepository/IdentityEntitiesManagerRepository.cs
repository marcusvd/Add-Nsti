using Authentication.Helpers;
using Authentication.Entities;
using Microsoft.Extensions.Logging;
using Authentication.Context;
using Microsoft.EntityFrameworkCore;
using Authentication.Exceptions;

namespace Authentication.Operations.CompanyUsrAcct;

public class IdentityEntitiesManagerRepository : IIdentityEntitiesManagerRepository
{
    private readonly ILogger<IdentityEntitiesManagerRepository> _logger;
    private readonly IdImDbContext _dbContext;
    private readonly AuthGenericValidatorServices _genericValidatorServices;

    public IdentityEntitiesManagerRepository(
          ILogger<IdentityEntitiesManagerRepository> logger,
          IdImDbContext dbContext,
          AuthGenericValidatorServices genericValidatorServices
      )
    {
        _dbContext = dbContext;
        _logger = logger;
        _genericValidatorServices = genericValidatorServices;
    }

    public async Task<UserAccount> GetUserAccountFull(string email)
    {
        var userAny = _dbContext.Users.AsNoTracking().Any();

        var invalidUser = new UserAccount
        {
            Id = -1,
            UserName = "Invalid",
            DisplayUserName = "Invalid@Invalid.com.br",
            Email = "Invalid@Invalid.com.br"
        };

        if (userAny)
            return await _dbContext.Users.AsNoTracking().Include(x => x.CompanyUserAccounts).FirstOrDefaultAsync(x => x.Email.Equals(email)) ?? invalidUser;

        return invalidUser;
    }
    public async Task<CompanyAuth> AddCompany(CompanyAuth companyAuth)
    {


        var companyAuthAdd = await _dbContext.AddAsync(companyAuth);

        // var invalidCompany = new CompanyAuth
        // {
        //     Id = -1,
        //     Name = "Invalid"
        // };


        if (await _dbContext.SaveChangesAsync() > 0) return companyAuthAdd.Entity;
        // {
        //     _genericValidatorServices.IsObjNull(companyAuth);

        //     // return await _dbContext.CompanyAuth.FirstOrDefaultAsync(x => x.Id == companyAuth.Id) ?? invalidCompany;
        // return companyAuthAdd.Entity;
        // }

              _logger.LogError("Company creation failed. Errors: {Errors}",
            companyAuth.Name, string.Join(", ", companyAuthAdd.Entity));

            throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenAddCompany);

    }
}