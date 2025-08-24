
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Register;
    public interface IRegisterServices
    {
        Task<UserToken> RegisterAsync(RegisterModel user);
    }