using System.Threading.Tasks;
using Authentication;


namespace Services.Services.Operations.Authentication
{
    public interface ITokenServices
    {
        Task<UserToken> GenerateToken(UserAccount user);
    }
}
