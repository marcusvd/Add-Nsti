using System;
using Application.Services.Operations.Inheritances;
using Application.Services.Operations.Inheritances.Enums;
using Application.Services.Shared.Dtos;
using Domain.Entities.Customers;

namespace Application.Services.Operations.Customers.Dtos
{
    public class CustomerDto : RootBaseDto
    {
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string CNPJ { get; set; }
        public EntityTypeEnumDto EntityType { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; }
        public ContactDto Contact { get; set; }
    }
}