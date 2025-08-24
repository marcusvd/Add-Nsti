using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Profiles.Dtos;

public class UserProfileDto : RootBaseDto
{
    public required string UserAccountId { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }

}
