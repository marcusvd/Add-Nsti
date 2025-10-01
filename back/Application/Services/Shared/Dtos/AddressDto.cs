using Domain.Entities.Shared;

namespace Application.Services.Shared.Dtos;

public class AddressDto : RootBase
{
    public required string ZipCode { get; set; } = string.Empty;
    public required string Street { get; set; } = string.Empty;
    public required string Number { get; set; } = string.Empty;
    public required string District { get; set; } = string.Empty;
    public required string City { get; set; } = string.Empty;
    public required string State { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
}

public static class AddressMapper
{
    public static Address ToEntity(this AddressDto addressDto)
    {
        Address address = new()
        {
            Id = addressDto.Id,
            Deleted = addressDto.Deleted,
            Registered = addressDto.Registered,
            ZipCode = addressDto.ZipCode,
            Street = addressDto.Street,
            Number = addressDto.Number,
            District = addressDto.District,
            City = addressDto.City,
            State = addressDto.State,
            Complement = addressDto.Complement,

        };

        return address;
    }

    public static AddressDto ToDto(this Address addressEntity)
    {
        AddressDto address = new()
        {
            Id = addressEntity.Id,
            Deleted = addressEntity.Deleted,
            Registered = addressEntity.Registered,
            ZipCode = addressEntity.ZipCode,
            Street = addressEntity.Street,
            Number = addressEntity.Number,
            District = addressEntity.District,
            City = addressEntity.City,
            State = addressEntity.State,
            Complement = addressEntity.Complement,
        };
        return address;
    }
    public static Address ToUpdate(this AddressDto dto, Address db)
    {
       
            db.Id = dto.Id;
            db.Deleted = dto.Deleted;
            db.Registered = dto.Registered;
            db.ZipCode = dto.ZipCode;
            db.Street = dto.Street;
            db.Number = dto.Number;
            db.District = dto.District;
            db.City = dto.City;
            db.State = dto.State;
            db.Complement = dto.Complement;

        return db;
    }

    public static AddressDto Incomplete()
    {
        AddressDto incomplete = new()
        {
            Id = 0,
            Deleted = DateTime.MinValue,
            Registered = DateTime.Now,
            ZipCode = "Cadastro Incompleto",
            Street = "Cadastro Incompleto",
            Number = "Cadastro Incompleto",
            District = "Cadastro Incompleto",
            City = "Cadastro Incompleto",
            State = "Cadastro Incompleto",
            Complement = "Cadastro Incompleto"
        };

        return incomplete;
    }


}