
namespace Application.Services.Shared.Dtos;

public abstract class RootBaseDto
{
    public virtual int Id { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
}