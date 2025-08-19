
using Authentication.Entities;

namespace Authentication.AuthenticationRepository.UserAccountRepository;

public interface IUserAccountRepository : IAuthRepository<UserAccount>
{
    Task<UserAccount> GetUserAccountFull(string email);
}