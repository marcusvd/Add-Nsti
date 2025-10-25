using Domain.Entities.Authentication;
using Domain.Entities.System.Businesses;
using Domain.Entities.System.Companies.extends;

namespace Domain.Entities.System.Companies;


public class CompanyAuth : CompanyBaseDb
{
    public required string Name { get; set; }
    public required string TradeName { get; set; }
    public int BusinessAuthId { get; set; }
    public BusinessAuth? BusinessAuth { get; set; }
    public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();
    public ICollection<Role> Roles { get; set; } = new List<Role>();

}

