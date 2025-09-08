
using Domain.Entities.Authentication;

namespace Repository.Data.Operations.AuthRepository.BusinessRepository;

public interface IBusinessAuthRepository : IAuthRepository<BusinessAuth>
{
    // Task<BusinessAuth> BusinessFullAsync(int id);
}