using Domain.Entities.Shared;
using Domain.Entities.System.BusinessesCompanies;

namespace Domain.Entities.System.Profiles;

public class UserProfile : RootBase
{
    public int BusinessProfileId { get; set; }
    public BusinessProfile? BusinessProfile { get; set; }
    public required string UserAccountId { get; set; }
    public virtual Address? Address { get; set; }
    public virtual Contact? Contact { get; set; }
}