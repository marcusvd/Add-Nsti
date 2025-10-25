
namespace Domain.Entities.Shared;

public abstract class RootBaseDb
{
    public int Id { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
}
