
namespace Application.Services.Operations.Auth.Dtos;


public class PasswordWillExpiresDto
{
    public int UserId { get; set; }
    public bool WillExpires { get; set; }
}
