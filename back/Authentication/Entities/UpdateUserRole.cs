
namespace Authentication.Entities;

public class UpdateUserRole
{
    public required string UserName { get; set; }
    public required string Role { get; set; }
    public bool Delete { get; set; }
}