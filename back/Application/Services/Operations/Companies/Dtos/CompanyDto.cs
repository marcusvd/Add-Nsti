using System.Collections.Generic;

using Application.Services.Operations.Customers.Dtos;
using Application.Services.Shared.Dtos;
using Application.Services.Operations.Auth.Dtos;

namespace Application.Services.Operations.Companies.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public bool Deleted { get; set; }
        public ContactDto Contact { get; set; }
        public List<UserAccountDto> UserAccounts { get; set; }
        public List<CustomerDto> Customers { get; set; }
    }

}
