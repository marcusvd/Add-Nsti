
using Domain.Entities.System.Businesses;

namespace Repository.Data.Operations.AuthRepository.BusinessRepository;

public interface IBusinessAuthRepository : IAuthRepository<BusinessAuth>
{
    // Task<BusinessAuth> BusinessFullAsync(int id);
}