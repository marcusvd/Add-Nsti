
namespace Application.Shared.Dtos;

public abstract class RootBaseDto
{
    public virtual int Id { get; set; }
    public virtual DateTime Deleted { get; set; } = DateTime.MinValue;
    public virtual DateTime Registered { get; set; } = DateTime.UtcNow;
}