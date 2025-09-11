
namespace Application.Services.Operations.Auth.Dtos;

public class UpdateUserRoleDto
{
    public required string UserName { get; set; }
    public required string Role { get; set; }
    public required string DisplayRole { get; set; }
    public bool Delete { get; set; }
}