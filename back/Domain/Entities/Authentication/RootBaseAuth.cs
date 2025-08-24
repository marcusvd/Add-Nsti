
namespace Domain.Entities.Authentication;

    public abstract class RootBaseAuth
    {
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
    }
