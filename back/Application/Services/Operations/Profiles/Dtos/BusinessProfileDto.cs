using Domain.Entities.Shared;
using Domain.Entities.System.Profiles;

namespace  Application.Services.Operations.Profiles.Dtos;

public class BusinessProfileDto:RootBase
{
    public required string BusinessAuthId { get; set; }

    // public ICollection<UserProfile> UsersAccounts { get; set; }  = new List<UserProfile>();
    // public ICollection<Company> Companies { get; set; } = new List<Company>();
}