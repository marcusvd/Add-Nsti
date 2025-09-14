
namespace Application.Services.Operations.Auth.Dtos;

public class AccessControlDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime WorkBreak { get; set; }
}