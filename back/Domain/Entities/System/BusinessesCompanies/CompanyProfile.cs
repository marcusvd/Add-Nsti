using Domain.Entities.Shared;
using Domain.Entities.System.Customers;

namespace Domain.Entities.System.BusinessesCompanies;

public class CompanyProfile : RootBase
{
    
    public required string CompanyAuthId { get; set; }
    public  int BusinessProfileId { get; set; }
    public required string CNPJ { get; set; }
    public BusinessProfile? BusinessProfile { get; set; }
    public Address? Address { get; set; }
    public Contact? Contact { get; set; } = new();
    public ICollection<CustomerCompany> CustomersCompanies { get; set; } = new List<CustomerCompany>();

}