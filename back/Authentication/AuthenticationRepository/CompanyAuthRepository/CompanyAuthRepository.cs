using Domain.Entities.Authentication;
using Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.AuthenticationRepository.BusinessRepository;

public class CompanyAuthRepository : AuthRepository<CompanyAuth>, ICompanyAuthRepository
{

    private readonly IdImDbContext _dbContext;

    public CompanyAuthRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}