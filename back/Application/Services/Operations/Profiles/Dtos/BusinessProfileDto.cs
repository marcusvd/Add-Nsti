using Application.Services.Operations.Companies.Dtos;
using Domain.Entities.Shared;
using Domain.Entities.System.Profiles;

namespace  Application.Services.Operations.Profiles.Dtos;

public class BusinessProfileDto:RootBase
{
    public required string BusinessAuthId { get; set; }

    public ICollection<UserProfileDto> UsersAccounts { get; set; }  = new List<UserProfileDto>();
    public ICollection<CompanyProfileDto> Companies { get; set; } = new List<CompanyProfileDto>();
}