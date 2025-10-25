using Domain.Entities.Shared;
using Domain.Entities.System.Businesses.extends;
using Domain.Entities.System.Companies;
using Domain.Entities.System.Profiles;

namespace Domain.Entities.System.Businesses;

public class BusinessProfile : BusinessBaseDb
{
    public required string BusinessAuthId { get; set; }
    public ICollection<UserProfile> UsersAccounts { get; set; } = new List<UserProfile>();
    public ICollection<CompanyProfile> Companies { get; set; } = new List<CompanyProfile>();
}

