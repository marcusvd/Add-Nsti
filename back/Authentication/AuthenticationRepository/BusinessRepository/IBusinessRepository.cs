
using Authentication.Entities;

namespace Authentication.AuthenticationRepository.BusinessRepository;

public interface IBusinessRepository : IAuthRepository<Business>
{
    Task<Business> GetBusinessFull(int id);
}