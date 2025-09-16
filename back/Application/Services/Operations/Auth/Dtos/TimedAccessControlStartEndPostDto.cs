
using Application.Services.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Dtos;

public class TimedAccessControlStartEndPostDto : RootBaseDto
{
    public int UserId { get; set; }
    public string Start { get; set; } = "00:00";
    public string End { get; set; } = "00:00";
}


public static class TimedAccessControlStartEndPostMapper
{
    private readonly static DateTime _now = DateTime.Now;
    public static TimedAccessControl ToPost(this TimedAccessControlStartEndPostDto dto)
    {

        int hourStart = int.Parse(dto.Start.Split(':')[0]);
        int minuteStart = int.Parse(dto.Start.Split(':')[1]);

        int hourEnd = int.Parse(dto.End.Split(':')[0]);
        int minuteEnd = int.Parse(dto.End.Split(':')[1]);

        TimedAccessControl TimedAccessControl = new()
        {
            Id = dto.Id,
            Deleted = DateTime.MinValue,
            Registered = _now,
            Start = new DateTime(_now.Year, _now.Month, _now.Day, hourStart, minuteStart, 0),
            End = new DateTime(_now.Year, _now.Month, _now.Day, hourEnd, minuteEnd, 0),
            
        };

        return TimedAccessControl;
    }
    public static TimedAccessControl ToUpdate(this TimedAccessControlStartEndPostDto dto, int timeAccessControlId)
    {

        int hourStart = int.Parse(dto.Start.Split(':')[0]);
        int minuteStart = int.Parse(dto.Start.Split(':')[1]);

        int hourEnd = int.Parse(dto.End.Split(':')[0]);
        int minuteEnd = int.Parse(dto.End.Split(':')[1]);

        TimedAccessControl TimedAccessControl = new()
        {
            Id = timeAccessControlId,
            Deleted = DateTime.MinValue,
            Registered = _now,
            Start = new DateTime(_now.Year, _now.Month, _now.Day, hourStart, minuteStart, 0),
            End = new DateTime(_now.Year, _now.Month, _now.Day, hourEnd, minuteEnd, 0),
            
        };

        return TimedAccessControl;
    }
}