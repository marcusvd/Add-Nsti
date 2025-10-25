// using Application.Shared.Dtos;
// using Domain.Entities.Shared;

// namespace Application.Shared.Mappers.BaseMappers;

// public class AddressEntityMapper : BaseMapper<Address, AddressDto>
// {
//     public override AddressDto Map(Address source)
//     {
//         if (source == null) return AddressMapper.Incomplete();

//         var destination = base.Map(source);

//         return destination;
//     }
// }

// public class AddressDtoMapper : BaseMapper<AddressDto, Address>
// {
//       public override Address Map(AddressDto source)
//     {
//         if (source == null) return AddressMapper.Incomplete().ToEntity();

//         var destination = base.Map(source);

//         return destination;
//     }
// }

