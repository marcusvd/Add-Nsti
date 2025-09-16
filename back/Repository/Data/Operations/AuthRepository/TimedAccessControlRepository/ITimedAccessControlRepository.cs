
using Domain.Entities.Authentication;

namespace Repository.Data.Operations.AuthRepository.BusinessRepository;

public interface ITimedAccessControlRepository : IAuthRepository<TimedAccessControl>
{
    // Task<BusinessAuth> BusinessFullAsync(int id);
}