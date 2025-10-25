
namespace Domain.Entities.Shared
{
    public class Address: RootBaseDb
    {
        public required string ZipCode { get; set; } = string.Empty;
        public required string Street { get; set; } = string.Empty;
        public required string Number { get; set; } = string.Empty;
        public required string District { get; set; } = string.Empty;
        public required string City { get; set; } = string.Empty;
        public required string State { get; set; } = string.Empty;
        public  string? Complement { get; set; }
    }

}