using Microsoft.AspNetCore.Identity;

using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Authentication.Helpers;
using Authentication.Jwt;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Auth.Account.dtos;
using Application.Exceptions;
using Application.Services.Operations.Auth.Dtos;


namespace Application.Services.Operations.Auth.Login;

public partial class LoginServices : AuthenticationBase, ILoginServices
{


    private async Task<TimedAccessControlDto> GetTimedAccessControl(int userId)
    {
        var userIdIncludeTAC = await _AUTH_SERVICES_INJECTION.UsersAccounts.GetUserAccountFull(userId);

        if (userIdIncludeTAC.Id == -1) throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);

        return userIdIncludeTAC?.TimedAccessControl?.ToDto() ?? (TimedAccessControlDto)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<TimedAccessControlDto>();
    }

    private async Task<bool> CheckTimeInterval(TimedAccessControlDto timedAccessControl)
    {
        DateTime NowDt = DateTime.Now;
        TimeSpan Now = DateTime.Now.TimeOfDay;
        TimeSpan Free = new DateTime(NowDt.Year, NowDt.Month, NowDt.Day, 00, 00, 00).TimeOfDay;
        TimeSpan start = timedAccessControl.Start.TimeOfDay;
        TimeSpan end = timedAccessControl.End.TimeOfDay;

        if (Free == start && Free == end) return true;//Sem restrição.

        if (start <= end)// Intervalo normal: ex. 08:00–18:00
            return Now >= timedAccessControl.Start.TimeOfDay && Now <= timedAccessControl.End.TimeOfDay;
        else// Intervalo que cruza a meia-noite: ex. 22:00–02:00
            return Now >= start || Now <= end;

    }

}