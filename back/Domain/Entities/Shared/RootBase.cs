
namespace Domain.Entities.Shared;

public abstract class RootBase
{
    public int Id { get; set; }
    // public int CompanyId { get; set; }
    // public Company? Company { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
}
