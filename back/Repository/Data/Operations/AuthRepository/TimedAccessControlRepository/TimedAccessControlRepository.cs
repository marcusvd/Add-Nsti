using Domain.Entities.Authentication;
using Repository.Data.Context.Auth;

namespace Repository.Data.Operations.AuthRepository.BusinessRepository;

public class TimedAccessControlRepository : AuthRepository<TimedAccessControl>, ITimedAccessControlRepository
{
    private readonly IdImDbContext _dbContext;

    public TimedAccessControlRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}