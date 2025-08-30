
using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Companies.Dtos;

public class CompanyProfileDto:RootBaseDto
{
     public  string BusinessProfileId { get; set; }
    public required string CompanyAuthId { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }
}

