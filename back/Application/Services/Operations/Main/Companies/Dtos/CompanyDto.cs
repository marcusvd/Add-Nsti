using System.Collections.Generic;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Operations.Main.Customers.Dtos;
using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Main.Companies.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public bool Deleted { get; set; }
        public ContactDto Contact { get; set; }
        public List<MyUserDto> MyUsers { get; set; }
        public List<CustomerDto> Customers { get; set; }
    }

}
