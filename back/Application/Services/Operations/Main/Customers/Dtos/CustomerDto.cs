using System;
using Application.Services.Operations.Main.Inheritances;
using Application.Services.Operations.Main.Inheritances.Enums;
using Application.Services.Shared.Dtos;
using Domain.Entities.Main.Customers;

namespace Application.Services.Operations.Main.Customers.Dtos
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