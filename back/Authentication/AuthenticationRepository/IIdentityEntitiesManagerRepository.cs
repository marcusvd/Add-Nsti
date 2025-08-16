
using Authentication.Entities;

namespace Authentication.Operations.CompanyUsrAcct;

public interface IIdentityEntitiesManagerRepository
{
    Task<UserAccount> GetUserAccountFull(string email);
    Task<CompanyAuth> AddCompany(CompanyAuth companyAuth);

     
}