
using Domain.Entities.Authentication;

namespace Application.Auth.Register.Extends;

public interface IRegisterServicesBase
{
    // Task<bool> SendUrlTokenEmailConfirmation(bool registerResult, UserAccount userAccount);
    Task ValidateUniqueUserCredentials(string userName, string email);
}