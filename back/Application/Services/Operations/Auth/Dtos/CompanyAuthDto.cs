using Application.Services.Operations.Auth.Dtos;
using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class CompanyAuthDto : RootBaseDto
{
    public required string CompanyProfileId { get; set; }
    public required string Name { get; set; }
    public required string TradeName { get; set; }
    public int BusinessId { get; set; }
    public BusinessAuthDto? Business { get; set; }
    public ICollection<CompanyUserAccountDto> CompanyUserAccounts { get; set; } = new List<CompanyUserAccountDto>();
}