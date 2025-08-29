
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Register;
    public interface IFirstRegisterBusinessServices
    {
        Task<UserToken> RegisterAsync(RegisterModel user);
    }