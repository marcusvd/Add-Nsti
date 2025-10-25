using Domain.Entities.Shared;

namespace Application.Shared.Dtos;

public static class ContactMappers
{

    public static Contact ToUpdate(this ContactDto dto, Contact db)
    {
        db.Id = dto.Id;
        db.Deleted = dto.Deleted;
        db.Registered = dto.Registered;
        db.Email = dto.Email;
        db.Site = dto.Site;
        db.Cel = dto.Cel;
        db.Zap = dto.Zap;
        db.Landline = dto.Landline;

        return db;
    }

}
