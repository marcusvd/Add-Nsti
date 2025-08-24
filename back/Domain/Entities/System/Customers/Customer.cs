
using Domain.Entities.Shared;

namespace Domain.Entities.System.Customers;

public class Customer : RootBase
{
  public required string Name { get; set; }
  public required string TradeName { get; set; }
  public required string CNPJ { get; set; }
  public EntityTypeEnum EntityType { get; set; }
  public string? Description { get; set; }
  public Address? Address { get; set; }
  public Contact? Contact { get; set; }
  public ICollection<CustomerCompany> CustomersCompanies { get; set; } = new List<CustomerCompany>();

}