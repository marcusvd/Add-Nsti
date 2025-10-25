using Domain.Entities.Shared;

namespace Application.Shared.Dtos;

public static class AddressMappers
{

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

}
