using Application.Services.Operations.Profiles.Dtos;
using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

// public class UserAccountProfileDto : RootBaseDto
// {
//     public required string Email { get; set; }
//     public required string DisplayUserName { get; set; }
//     public required string UserName { get; set; }
//     public required AddressDto Address { get; set; }
//     public required ContactDto Contact { get; set; }

// }
public class UserAuthProfileDto : RootBaseDto
{
    public override int Id { get; set; } = -1;
    public required UserAccountDto UserAccountAuth { get; set; }
    public required UserProfileDto UserAccountProfile { get; set; }

}