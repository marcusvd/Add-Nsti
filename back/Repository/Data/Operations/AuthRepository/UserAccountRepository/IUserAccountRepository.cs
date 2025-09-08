
using System.Threading.Tasks;
using Domain.Entities.Authentication;

namespace Repository.Data.Operations.AuthRepository.UserAccountRepository;

public interface IUserAccountRepository : IAuthRepository<UserAccount>
{
    Task<UserAccount> GetUserAccountFull(string email);
}