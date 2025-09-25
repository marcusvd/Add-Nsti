
namespace Application.Services.Operations.Auth.Account.dtos;

public class LoginModelDto
{
    public string? UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; } = true;
}