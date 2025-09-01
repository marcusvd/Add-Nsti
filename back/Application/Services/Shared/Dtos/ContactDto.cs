using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.Shared;

namespace Application.Services.Shared.Dtos;

public class ContactDto : RootBase
{
    public ContactDto() { }

    public string? Email { get; set; }
    public string? Site { get; set; }
    public string? Cel { get; set; }
    public string? Zap { get; set; }
    public string? Landline { get; set; }
    public List<SocialNetworkDto>? SocialMedias { get; set; }

}


public static class ContactMapper
{

    public static Contact ToEntity(this ContactDto contactDto)
    {
        Contact contact = new()
        {
            Id = contactDto.Id,
            Deleted = contactDto.Deleted,
            Registered = contactDto.Registered,
            Email = contactDto.Email,
            Site = contactDto.Site,
            Cel = contactDto.Cel,
            Zap = contactDto.Zap,
            Landline = contactDto.Landline,

        };

        return contact;
    }

    public static ContactDto ToDto(this Contact contactEntity)
    {
        ContactDto contact = new()
        {
            Id = contactEntity.Id,
            Deleted = contactEntity.Deleted,
            Registered = contactEntity.Registered,
            Email = contactEntity.Email,
            Site = contactEntity.Site,
            Cel = contactEntity.Cel,
            Zap = contactEntity.Zap,
            Landline = contactEntity.Landline,

        };

        return contact;
    }

    public static ContactDto Incomplete()
    {
        ContactDto incomplete = new()
        {
            Id = 0,
            Deleted = DateTime.MinValue,
            Registered = DateTime.Now,
            Email = "Cadastro Incompleto",
            Site = "Cadastro Incompleto",
            Cel = "Cadastro Incompleto",
            Zap = "Cadastro Incompleto",
            Landline = "Cadastro Incompleto",
        };

        return incomplete;
    }


}