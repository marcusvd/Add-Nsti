using Application.Services.Operations.Companies.Dtos;
using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class BusinessProfileUpdateAddCompanyDto : RootBaseDto
{
    public required string Name { get; set; } = "Group business";
    public required string BusinessProfileId { get; set; }
    public CompanyProfileDto? Company { get; set; }
}