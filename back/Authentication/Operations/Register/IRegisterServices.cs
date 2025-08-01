
using Authentication.Entities;

namespace Authentication.Operations.Register
{
    public interface IRegisterServices
    {
        Task<UserToken> RegisterAsync(RegisterModel user);
    }
}