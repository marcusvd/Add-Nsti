
using Domain.Entities.Authentication;

namespace Authentication.AuthenticationRepository.BusinessAuthRepository;

public interface IBusinessAuthRepository : IAuthRepository<BusinessAuth>
{
    Task<BusinessAuth> GetBusinessFull(int id);
}