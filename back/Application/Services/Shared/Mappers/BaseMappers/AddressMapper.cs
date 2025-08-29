using Application.Services.Shared.Dtos;
using Domain.Entities.Shared;

namespace Application.Services.Shared.Mappers.BaseMappers;

public class AddressEntityMapper : BaseMapper<Address, AddressDto>
{
    public override AddressDto Map(Address source)
    {
        if (source == null) return new AddressDto();

        var destination = base.Map(source);

        return destination;
    }
}

public class AddressDtoMapper : BaseMapper<AddressDto, Address>
{
      public override Address Map(AddressDto source)
    {
        if (source == null) return new Address();

        var destination = base.Map(source);

        return destination;
    }
}

