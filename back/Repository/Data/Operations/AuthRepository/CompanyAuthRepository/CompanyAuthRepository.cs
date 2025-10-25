using Domain.Entities.Authentication;
using Domain.Entities.System.Companies;
using Microsoft.EntityFrameworkCore;
using Repository.Data.Context.Auth;

namespace Repository.Data.Operations.AuthRepository.BusinessRepository;

public class CompanyAuthRepository : AuthRepository<CompanyAuth>, ICompanyAuthRepository
{

    private readonly IdImDbContext _dbContext;

    public CompanyAuthRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}