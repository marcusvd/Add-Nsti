
using Domain.Entities.Shared;

namespace Domain.Entities.Authentication;

public class TimedAccessControl : RootBaseDb
{
    public DateTime Start { get; set; } = DateTime.MinValue;
    public DateTime End { get; set; } = DateTime.MinValue;
    public DateTime WorkBreakStart { get; set; } = DateTime.MinValue;
    public DateTime WorkBreakEnd { get; set; } = DateTime.MinValue;

}
