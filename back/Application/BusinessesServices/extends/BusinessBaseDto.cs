
namespace Application.BusinessesServices.Extends;

public abstract class BusinessBaseDto
{
    public int Id { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;

}