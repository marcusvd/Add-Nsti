using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class BusinessAuthDto : RootBaseDto
{

    public required string Name { get; set; }
    public required string BusinessProfileId { get; set; }
    public ICollection<UserAccountDto> UsersAccounts { get; set; } = new List<UserAccountDto>();
    public ICollection<CompanyAuthDto> Companies { get; set; } = new List<CompanyAuthDto>();


}