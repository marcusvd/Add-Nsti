using Application.Services.Operations.Main.Inheritances.Enums;
using Application.Services.Shared.Dtos;


namespace Application.Services.Operations.Main.Inheritances
{
    public class MainEntitiesBaseDto:RootBaseDto
    {
        public string Name { get; set; }
        public string Responsible { get; set; }
        public string CNPJ { get; set; }
        public string Description { get; set; }
        public string BusinessLine { get; set; }
         public EntityTypeEnumDto EntityType { get; set; }
        public AddressDto Address { get; set; }
        public ContactDto Contact { get; set; }
        public PhysicallyMovingCostsDto PhysicallyMovingCosts { get; set; }
        // public List<DestinyDto> CollectDeliverDestinies { get; set; }
        // public List<BillingFromDto> BillingFromCollectsDelivers { get; set; }
        // public List<QuantityDto> ProductsQuantities { get; set; }
    }


}
