using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class BusinessAuthUpdateAddCompanyDto : RootBaseDto
{

    public required string BusinessProfileId { get; set; }
    public CompanyAuthDto? Company { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }
}