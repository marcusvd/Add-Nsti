using System;
using Application.Auth.Extends;
using Application.Inheritances.Enums;
using Application.Shared.Dtos;
using Domain.Entities.System.Customers;

namespace Application.Customers.Dtos
{
    public class CustomerDto : RootBaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public EntityTypeEnumDto EntityType { get; set; } = EntityTypeEnumDto.PJ;
        public string Description { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new AddressDto();
        public ContactDto Contact { get; set; } = new ContactDto();
    }
}