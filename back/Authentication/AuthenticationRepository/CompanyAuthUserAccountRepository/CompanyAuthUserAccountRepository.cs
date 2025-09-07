using Domain.Entities.Authentication;
using Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.AuthenticationRepository.BusinessRepository;

public class CompanyAuthUserAccountRepository : AuthRepository<CompanyUserAccount>, ICompanyAuthUserAccountRepository
{

    private readonly IdImDbContext _dbContext;

    public CompanyAuthUserAccountRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}