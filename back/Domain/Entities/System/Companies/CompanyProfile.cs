using Domain.Entities.Shared;
using Domain.Entities.System.Businesses;
using Domain.Entities.System.Companies.extends;

namespace Domain.Entities.System.Companies;

public class CompanyProfile : CompanyBaseDb
{
    public  int BusinessProfileId { get; set; }
    public BusinessProfile? BusinessProfile { get; set; }
    public Address? Address { get; set; }
    public Contact? Contact { get; set; } = new();
    public ICollection<CustomerCompany> CustomersCompanies { get; set; } = new List<CustomerCompany>();

}