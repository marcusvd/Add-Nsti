using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class BusinessAuthDto : RootBaseDto
{
    public required string Name { get; set; } = "Group business";
    public required string BusinessProfileId { get; set; } 
    // public ICollection<UserAccount> UsersAccounts { get; set; } = new List<UserAccount>();
    // public ICollection<CompanyAuth> Companies { get; set; } = new List<CompanyAuth>();
}