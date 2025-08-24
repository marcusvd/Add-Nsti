
using Domain.Entities.Authentication;

namespace Authentication.AuthenticationRepository.UserAccountRepository;

public interface IUserAccountRepository : IAuthRepository<UserAccount>
{
    Task<UserAccount> GetUserAccountFull(string email);
}