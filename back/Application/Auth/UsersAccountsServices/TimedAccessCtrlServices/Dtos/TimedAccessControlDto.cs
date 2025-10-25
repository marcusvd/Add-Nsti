
using Application.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Dtos;

public class TimedAccessControlDto : RootBaseDto
{
    public int UserId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime WorkBreakStart { get; set; }
    public DateTime WorkBreakEnd { get; set; }


    public static implicit operator TimedAccessControl(TimedAccessControlDto dto)
    {
        return new TimedAccessControl
        {
            Start = dto.Start,
            End = dto.End,
            WorkBreakStart = dto.WorkBreakStart,
            WorkBreakEnd = dto.WorkBreakEnd,
        };
    }
    public static implicit operator TimedAccessControlDto(TimedAccessControl dto)
    {
         return new TimedAccessControl
         {
            Id = dto.Id,
            Start = dto.Start,
            End = dto.End,
            WorkBreakStart = dto.WorkBreakStart,
            WorkBreakEnd = dto.WorkBreakEnd,
        };
    }

}


// public static class TimedAccessControlMapper
// {
//     public static TimedAccessControl ToEntity(this TimedAccessControlDto dto)
//     {
//         TimedAccessControl TimedAccessControl = new()
//         {
//             Id = dto.Id,
//             Deleted = dto.Deleted,
//             Registered = dto.Registered,
//             Start = dto.Start,
//             End = dto.End,
//             WorkBreakStart = dto.WorkBreakStart,
//             WorkBreakEnd = dto.WorkBreakEnd
//         };

//         return TimedAccessControl;
//     }

//     public static TimedAccessControlDto ToDto(this TimedAccessControl entity)
//     {
//         TimedAccessControlDto TimedAccessControl = new()
//         {
//             Id = entity.Id,
//             Deleted = entity.Deleted,
//             Registered = entity.Registered,
//             Start = entity.Start,
//             End = entity.End,
//             WorkBreakStart = entity.WorkBreakStart,
//             WorkBreakEnd = entity.WorkBreakEnd

//         };
//         return TimedAccessControl;
//     }

//     public static TimedAccessControlDto Incomplete()
//     {
//         TimedAccessControlDto incomplete = new()
//         {
//             Id = 0,
//             Deleted = DateTime.MinValue,
//             Registered = DateTime.Now,
//             Start = DateTime.MinValue,
//             End = DateTime.MinValue,
//             WorkBreakStart = DateTime.MinValue,
//             WorkBreakEnd = DateTime.MinValue
//         };

//         return incomplete;
//     }
// }