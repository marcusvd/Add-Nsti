using Domain.Entities.Shared;

namespace Application.Shared.Dtos;

public class ContactDto : RootBaseDto
{
    public ContactDto() { }

    public string? Email { get; set; }
    public string? Site { get; set; }
    public string? Cel { get; set; }
    public string? Zap { get; set; }
    public string? Landline { get; set; }
    public List<SocialNetworkDto>? SocialMedias { get; set; }

    public static implicit operator Contact(ContactDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            Email = dto.Email,
            Site = dto.Site,
            Cel = dto.Cel,
            Zap = dto.Zap,
            Landline = dto.Landline,
            SocialMedias = null

        };
    }
    public static implicit operator ContactDto(Contact dto)
    {
        return new()
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            Email = dto.Email,
            Site = dto.Site,
            Cel = dto.Cel,
            Zap = dto.Zap,
            Landline = dto.Landline,
            SocialMedias = null

        };
    }

}


// public static class ContactMapper
// {
//     public static Contact ToUpdate(this ContactDto dto, Contact db)
//     {
//         db.Id = dto.Id;
//         db.Deleted = dto.Deleted;
//         db.Registered = dto.Registered;
//         db.Email = dto.Email;
//         db.Site = dto.Site;
//         db.Cel = dto.Cel;
//         db.Zap = dto.Zap;
//         db.Landline = dto.Landline;

//         return db;
//     }

    // public static Contact ToEntity(this ContactDto contactDto)
    // {
    //     Contact contact = new()
    //     {
    //         Id = contactDto.Id,
    //         Deleted = contactDto.Deleted,
    //         Registered = contactDto.Registered,
    //         Email = contactDto.Email,
    //         Site = contactDto.Site,
    //         Cel = contactDto.Cel,
    //         Zap = contactDto.Zap,
    //         Landline = contactDto.Landline,

    //     };

    //     return contact;
    // }

    // public static ContactDto ToDto(this Contact contactEntity)
    // {
    //     ContactDto contact = new()
    //     {
    //         Id = contactEntity.Id,
    //         Deleted = contactEntity.Deleted,
    //         Registered = contactEntity.Registered,
    //         Email = contactEntity.Email,
    //         Site = contactEntity.Site,
    //         Cel = contactEntity.Cel,
    //         Zap = contactEntity.Zap,
    //         Landline = contactEntity.Landline,

    //     };

    //     return contact;
    // }


    // public static ContactDto Incomplete()
    // {
    //     ContactDto incomplete = new()
    //     {
    //         Id = 0,
    //         Deleted = DateTime.MinValue,
    //         Registered = DateTime.Now,
    //         Email = "Cadastro Incompleto",
    //         Site = "Cadastro Incompleto",
    //         Cel = "Cadastro Incompleto",
    //         Zap = "Cadastro Incompleto",
    //         Landline = "Cadastro Incompleto",
    //     };

    //     return incomplete;
    // }


// }