
namespace Domain.Entities.Authentication;

public class LoginModel
{
    public string? UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}