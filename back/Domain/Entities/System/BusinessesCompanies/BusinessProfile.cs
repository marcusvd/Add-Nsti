using Domain.Entities.Shared;
using Domain.Entities.System.Profiles;

namespace  Domain.Entities.System.BusinessesCompanies;

public class BusinessProfile : RootBase
{
    public required int Id { get; set; }
    public required string BusinessAuthId { get; set; }

    public ICollection<UserProfile> UsersAccounts { get; set; } = new List<UserProfile>();
    public ICollection<CompanyProfile> Companies { get; set; } = new List<CompanyProfile>();
}

