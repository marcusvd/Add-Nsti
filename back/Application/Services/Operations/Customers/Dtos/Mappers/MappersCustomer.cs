using System.Collections.Generic;


using Application.Services.Operations.Inheritances.Enums;
using Application.Services.Shared.Dtos.Mappers;
using Domain.Entities.Customers;

namespace Application.Services.Operations.Customers.Dtos.Mappers;

public partial class CustomerObjectMapperServices : CommonObjectMapper, ICustomerObjectMapperServices
{
    public List<CustomerDto> CustomerListMake(List<Customer> list)
    {
        if (list == null) return null;

        var toReturn = new List<CustomerDto>();

        list.ForEach(x =>
        {
            toReturn.Add(CustomerMapper(x));
        });

        return toReturn;
    }
    public List<Customer> CustomerListMake(List<CustomerDto> list)
    {
        if (list == null) return null;

        var toReturn = new List<Customer>();

        list.ForEach(x =>
        {
            toReturn.Add(CustomerMapper(x));
        });


        return toReturn;
    }
    public CustomerDto CustomerMapper(Customer entity)
    {
        if (entity == null) return null;

        var obj = new CustomerDto()
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            Deleted = entity.Deleted,
            Registered = entity.Registered,
            Name = entity.Name,
            CNPJ = entity.CNPJ,
            EntityType = (EntityTypeEnumDto)entity.EntityType,
            Description = entity.Description,
            Address = AddressMapper(entity.Address),
            Contact = ContactMapper(entity.Contact),

        };

        return obj;
    }
    public Customer CustomerMapper(CustomerDto entity)
    {
        if (entity == null) return null;

        var obj = new Customer()
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            Deleted = entity.Deleted,
            Registered = entity.Registered,

            Name = entity.Name,
            CNPJ = entity.CNPJ,
            EntityType = (EntityTypeEnum)entity.EntityType,
            Description = entity.Description,
            Address = AddressMapper(entity.Address),
            Contact = ContactMapper(entity.Contact),

        };

        return obj;
    }
    public Customer CustomerUpdateMapper(CustomerDto dto, Customer db)
    {
        if (dto == null) return null;
        if (db == null) return null;

        db.Id = dto.Id;
        db.CompanyId = dto.CompanyId;
        db.Name = dto.Name;
        db.CNPJ = dto.CNPJ;
        db.EntityType = (EntityTypeEnum)dto.EntityType;
        db.Description = dto.Description;
        db.Address = AddressMapper(dto.Address);
        db.Contact = ContactMapper(dto.Contact);

        return db;
    }
}
