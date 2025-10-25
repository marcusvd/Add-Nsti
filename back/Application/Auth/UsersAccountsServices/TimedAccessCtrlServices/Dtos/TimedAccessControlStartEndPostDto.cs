
using Application.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Dtos;

public class TimedAccessControlStartEndPostDto : RootBaseDto
{
    public int UserId { get; set; }
    public string Start { get; set; } = "00:00";
    public string End { get; set; } = "00:00";
}


public static class TimedAccessControlStartEndPostMapper
{
    private readonly static DateTime _now = DateTime.Now;
    private static int hourStart = 0;
    private static int minuteStart = 0;
    private static int hourEnd = 0;
    private static int minuteEnd = 0;
    public static TimedAccessControl ToPost(this TimedAccessControlStartEndPostDto dto)
    {
        BuilderTimerStartEnd(dto);

        return TacBuilder(dto.Id);

        // int hourStart = int.Parse(dto.Start.Split(':')[0]);
        // int minuteStart = int.Parse(dto.Start.Split(':')[1]);

        // int hourEnd = int.Parse(dto.End.Split(':')[0]);
        // int minuteEnd = int.Parse(dto.End.Split(':')[1]);

        // TimedAccessControl TimedAccessControl = new()
        // {
        //     Id = dto.Id,
        //     Deleted = DateTime.MinValue,
        //     Registered = _now,
        //     Start = new DateTime(_now.Year, _now.Month, _now.Day, hourStart, minuteStart, 0),
        //     End = new DateTime(_now.Year, _now.Month, _now.Day, hourEnd, minuteEnd, 0),

        // };

        // return TimedAccessControl;
    }
    public static TimedAccessControl ToUpdate(this TimedAccessControlStartEndPostDto dto, int timeAccessControlId)
    {

        BuilderTimerStartEnd(dto);
        return TacBuilder(timeAccessControlId);
        // int hourStart = int.Parse(dto.Start.Split(':')[0]);
        // int minuteStart = int.Parse(dto.Start.Split(':')[1]);

        // int hourEnd = int.Parse(dto.End.Split(':')[0]);
        // int minuteEnd = int.Parse(dto.End.Split(':')[1]);


        // TimedAccessControl TimedAccessControl = new()
        // {
        //     Id = timeAccessControlId,
        //     Deleted = DateTime.MinValue,
        //     Registered = _now,
        //     Start = new DateTime(_now.Year, _now.Month, _now.Day, hourStart, minuteStart, 0),
        //     End = new DateTime(_now.Year, _now.Month, _now.Day, hourEnd, minuteEnd, 0),

        // };

    }

    private static void BuilderTimerStartEnd(TimedAccessControlStartEndPostDto dto)
    {
        hourStart = int.Parse(dto.Start.Split(':')[0]);
        minuteStart = int.Parse(dto.Start.Split(':')[1]);

        hourEnd = int.Parse(dto.End.Split(':')[0]);
        minuteEnd = int.Parse(dto.End.Split(':')[1]);
    }


    private static TimedAccessControl TacBuilder(int id)
    {
        return new()
        {
            Id = id,
            Deleted = DateTime.MinValue,
            Registered = _now,
            Start = new DateTime(_now.Year, _now.Month, _now.Day, hourStart, minuteStart, 0),
            End = new DateTime(_now.Year, _now.Month, _now.Day, hourEnd, minuteEnd, 0),

        };
    }
}