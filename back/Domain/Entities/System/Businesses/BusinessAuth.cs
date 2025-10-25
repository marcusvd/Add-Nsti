using Domain.Entities.Authentication;
using Domain.Entities.Shared;
using Domain.Entities.System.Businesses.extends;
using Domain.Entities.System.Companies;

namespace Domain.Entities.System.Businesses;

public class BusinessAuth : BusinessBaseDb
{
    public required string Name { get; set; }
    public required string BusinessProfileId { get; set; }
    public ICollection<UserAccount> UsersAccounts { get; set; } = new List<UserAccount>();
    public ICollection<CompanyAuth> Companies { get; set; } = new List<CompanyAuth>();
    
}

