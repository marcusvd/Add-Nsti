using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Main.Inheritances
{
    public class PhysicallyMovingCostsDto : RootBaseDto
    {
        public decimal Fuel { get; set; }
        public decimal Apps { get; set; }
        public decimal PublicTransport { get; set; }
        public decimal MotoBoy { get; set; }
    }
}