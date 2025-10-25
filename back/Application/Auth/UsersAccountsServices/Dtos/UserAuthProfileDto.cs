using Application.Auth.UsersAccountsServices.Dtos;
using Application.Shared.Dtos;
namespace Application.UsersAccountsServices.Dtos;

public class UserAuthProfileDto : RootBaseDto
{
    public override int Id { get; set; } = -1;
    public required UserAccountDto UserAccountAuth { get; set; }
    public required UserProfileDto UserAccountProfile { get; set; }

}