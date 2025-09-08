using Domain.Entities.Authentication;
using Repository.Data.Context.Auth;

namespace Repository.Data.Operations.AuthRepository.BusinessRepository;

public class BusinessAuthRepository : AuthRepository<BusinessAuth>, IBusinessAuthRepository
{

    private readonly IdImDbContext _dbContext;

    public BusinessAuthRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}