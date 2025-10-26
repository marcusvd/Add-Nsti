using Application.Shared.Dtos;
using UnitOfWork.Persistence.Operations;
using Microsoft.AspNetCore.Identity;
using Application.Helpers.Inject;
using Authentication.Exceptions;
using Application.Exceptions;
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Dtos;
using Domain.Entities.Authentication;
using Application.Auth.UsersAccountsServices.Extends;


namespace Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Services;

public class TimedAccessControlServices : UserAccountServicesBase, ITimedAccessControlServices
{
  private readonly IUnitOfWork _genericRepo;
   private readonly UserManager<UserAccount> _userManager;
  private readonly IValidatorsInject _validatorsInject;
  private readonly IUserAccountAuthServices _userAccountAuthServices;
  private static DateTime Start { get; set; }
  private static DateTime End { get; set; }
  public TimedAccessControlServices(
        IUnitOfWork genericRepo,
        UserManager<UserAccount> userManager,
        IValidatorsInject validatorsInject,
        IUserAccountAuthServices userAccountAuthServices
    )
  {
    _genericRepo = genericRepo;
     _userManager = userManager;
    _validatorsInject = validatorsInject;
    _userAccountAuthServices = userAccountAuthServices;
  }

  public async Task<IdentityResult> TimedAccessControlStartEndUpdateAsync(TimedAccessControlStartEndPostDto timedAccessControl)
  {

    ValidateUserId(timedAccessControl.UserId);
    
    var userAccount = await _userAccountAuthServices.GetUserIncluded(timedAccessControl.UserId);

    _validatorsInject.GenericValidators.IsObjNull(timedAccessControl);

    var id = userAccount?.TimedAccessControl?.Id;

    if (userAccount?.TimedAccessControl?.Id > 0)
    {
      _genericRepo.TimedAccessControls.Update(timedAccessControl.ToUpdate(id ?? throw new AuthServicesException(GlobalErrorsMessagesException.IdIsNull)));

      if (await _genericRepo.SaveID())
        return IdentityResult.Success;
    }
    else
      userAccount = AssignValue(userAccount, timedAccessControl);

    return await _userManager.UpdateAsync(userAccount);
  }

  private UserAccount AssignValue(UserAccount userAccount, TimedAccessControlStartEndPostDto tac)
  {
    userAccount.TimedAccessControl = tac.ToPost();
    return userAccount;
  }

  public async Task<TimedAccessControlDto> GetTimedAccessControlAsync(int userId)
  {

    BuilderTimer();

    var times = await _userAccountAuthServices.GetUserIncluded(userId);

    _validatorsInject.GenericValidators.IsObjNull(times);

    return times.TimedAccessControl ?? new TimedAccessControlDto() { Start = Start, End = End };
  }

  private static void BuilderTimer()
  {
    var _now = DateTime.Now;
    Start = new DateTime(_now.Year, _now.Month, _now.Day, 00, 00, 00);
    End = new DateTime(_now.Year, _now.Month, _now.Day, 00, 00, 00);
  }

  public async Task<bool> CheckTimeIntervalAsync(TimedAccessControlDto timedAccessControl)
  {
    DateTime NowDt = DateTime.Now;
    TimeSpan Now = DateTime.Now.TimeOfDay;
    TimeSpan Free = new DateTime(NowDt.Year, NowDt.Month, NowDt.Day, 00, 00, 00).TimeOfDay;
    TimeSpan start = timedAccessControl.Start.TimeOfDay;
    TimeSpan end = timedAccessControl.End.TimeOfDay;

    if (Free == start && Free == end) return true;//Sem restrição.

    if (start <= end)// Intervalo normal: ex. 08:00–18:00
      return await Task.FromResult(Now >= timedAccessControl.Start.TimeOfDay && Now <= timedAccessControl.End.TimeOfDay);
    else// Intervalo que cruza a meia-noite: ex. 22:00–02:00
      return await Task.FromResult(Now >= start || Now <= end);

  }
}