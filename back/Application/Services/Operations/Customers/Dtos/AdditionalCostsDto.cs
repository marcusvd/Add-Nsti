
using Application.Services.Shared.Dtos;

namespace Domain.Entities.Main.Customers
{

    public class AdditionalCostsDto: RootBaseDto
    {
        public AdditionalCostsDto() { }
        public AdditionalCostsDto(decimal fixedPhysicallyMovingCosts)
        {
            FixedPhysicallyMovingCosts = fixedPhysicallyMovingCosts;
        }
        public decimal FixedPhysicallyMovingCosts { get; set; }

    }
}
