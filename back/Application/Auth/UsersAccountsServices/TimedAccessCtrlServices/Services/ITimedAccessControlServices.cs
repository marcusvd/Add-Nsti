
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Dtos;
using Microsoft.AspNetCore.Identity;


namespace Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Services;

public interface ITimedAccessControlServices
{
    Task<IdentityResult> TimedAccessControlStartEndUpdateAsync(TimedAccessControlStartEndPostDto timedAccessControl);
    Task<TimedAccessControlDto> GetTimedAccessControlAsync(int userId);
    Task<bool> CheckTimeIntervalAsync(TimedAccessControlDto timedAccessControl);
    
}