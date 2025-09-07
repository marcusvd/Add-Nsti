using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class AddressUserAccountProfileUpdateDto : RootBaseDto
{
    public required string UserAccountId { get; set; }
    public required AddressDto Address { get; set; }
}
