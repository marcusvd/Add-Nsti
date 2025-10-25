using Domain.Entities.Shared;

namespace Application.Shared.Dtos;

public class AddressDto : RootBaseDb
{
    public string ZipCode { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;

    public static implicit operator Address(AddressDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            ZipCode = dto.ZipCode,
            Street = dto.Street,
            Number = dto.Number,
            District = dto.District,
            City = dto.City,
            State = dto.State,
            Complement = dto.Complement,
        };
    }
    public static implicit operator AddressDto(Address dto)
    {
        return new()
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            ZipCode = dto.ZipCode,
            Street = dto.Street,
            Number = dto.Number,
            District = dto.District,
            City = dto.City,
            State = dto.State,
            Complement = dto.Complement,
        };
    }
    
}

// public static class AddressMapper
// {
//     public static Address ToEntity(this AddressDto addressDto)
//     {
//         Address address = new()
//         {
//             Id = addressDto.Id,
//             Deleted = addressDto.Deleted,
//             Registered = addressDto.Registered,
//             ZipCode = addressDto.ZipCode,
//             Street = addressDto.Street,
//             Number = addressDto.Number,
//             District = addressDto.District,
//             City = addressDto.City,
//             State = addressDto.State,
//             Complement = addressDto.Complement,

//         };

//         return address;
//     }

//     public static AddressDto ToDto(this Address addressEntity)
//     {
//         AddressDto address = new()
//         {
//             Id = addressEntity.Id,
//             Deleted = addressEntity.Deleted,
//             Registered = addressEntity.Registered,
//             ZipCode = addressEntity.ZipCode,
//             Street = addressEntity.Street,
//             Number = addressEntity.Number,
//             District = addressEntity.District,
//             City = addressEntity.City,
//             State = addressEntity.State,
//             Complement = addressEntity.Complement,
//         };
//         return address;
//     }
//     public static AddressDto Incomplete()
//     {
//         AddressDto incomplete = new()
//         {
//             Id = 0,
//             Deleted = DateTime.MinValue,
//             Registered = DateTime.Now,
//             ZipCode = "Cadastro Incompleto",
//             Street = "Cadastro Incompleto",
//             Number = "Cadastro Incompleto",
//             District = "Cadastro Incompleto",
//             City = "Cadastro Incompleto",
//             State = "Cadastro Incompleto",
//             Complement = "Cadastro Incompleto"
//         };

//         return incomplete;
//     }


// }