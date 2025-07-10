
using Domain.Entities.Shared;
namespace Domain.Entities.Customers;

public class Customer : RootBase
{
    public string Name { get; set; }
    public string TradeName { get; set; }
    public string CNPJ { get; set; }
    public EntityTypeEnum EntityType { get; set; }
    public string Description { get; set; }
    public Address Address { get; set; }
    public Contact Contact { get; set; }

}