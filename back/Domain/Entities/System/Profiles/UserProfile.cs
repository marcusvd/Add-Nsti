using Domain.Entities.Authentication.extends;
using Domain.Entities.Shared;
using Domain.Entities.System.Businesses;


namespace Domain.Entities.System.Profiles;

public class UserProfile : UserAccountBaseDb
{
    public int BusinessProfileId { get; set; }
    public BusinessProfile? BusinessProfile { get; set; }
    public required string UserAccountId { get; set; }
    public virtual Address? Address { get; set; }
    public virtual Contact? Contact { get; set; }
}