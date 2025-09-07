using Domain.Entities.Authentication;
using Authentication.Context;

namespace Authentication.AuthenticationRepository.BusinessAuthRepository;

public class BusinessAuthRepository : AuthRepository<BusinessAuth>, IBusinessAuthRepository
{

    private readonly IdImDbContext _dbContext;

    public BusinessAuthRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}