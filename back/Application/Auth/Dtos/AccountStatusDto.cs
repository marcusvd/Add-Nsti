
namespace Application.Auth.Dtos;

public class AccountStatusDto
{
    public bool IsEmailConfirmed { get; set; }
    public bool IsAccountLockedOut { get; set; }
}