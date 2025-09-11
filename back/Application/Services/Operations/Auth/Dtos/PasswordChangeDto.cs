
namespace Application.Services.Operations.Auth.Dtos;

public class PasswordChangeDto
{

    public required int UserId { get; set; }
    public required string CurrentPwd { get; set; }
    public required string Password { get; set; }

}