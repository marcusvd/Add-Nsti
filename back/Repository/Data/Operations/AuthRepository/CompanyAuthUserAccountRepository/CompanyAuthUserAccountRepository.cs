using Domain.Entities.Authentication;
using Repository.Data.Context.Auth;


namespace Repository.Data.Operations.AuthRepository.BusinessRepository;

public class CompanyAuthUserAccountRepository : AuthRepository<CompanyUserAccount>, ICompanyAuthUserAccountRepository
{

    private readonly IdImDbContext _dbContext;

    public CompanyAuthUserAccountRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}