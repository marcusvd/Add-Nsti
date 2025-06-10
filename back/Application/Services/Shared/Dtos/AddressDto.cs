using Domain.Entities.Shared;

namespace Application.Services.Shared.Dtos
{
    public class AddressDto: RootBase
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
    }
}