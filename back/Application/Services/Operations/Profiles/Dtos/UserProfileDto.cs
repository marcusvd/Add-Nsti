using Application.Services.Shared.Dtos;
using Domain.Entities.System.Profiles;

namespace Application.Services.Operations.Profiles.Dtos;

public class UserProfileDto : RootBaseDto
{
    public required string UserAccountId { get; set; }
    public int BusinessProfileId { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }

}

public static class UserProfileMapper
{
    public static UserProfile ToEntity(this UserProfileDto userAccountDto)
    {

        if (userAccountDto.Address is null)
            userAccountDto.Address = AddressMapper.Incomplete();

        if (userAccountDto.Contact is null)
            userAccountDto.Contact = ContactMapper.Incomplete();


        UserProfile userAccount = new()
        {
            Id = userAccountDto.Id,
            UserAccountId = userAccountDto.UserAccountId,
            Deleted = userAccountDto.Deleted,
            Registered = userAccountDto.Registered,
            BusinessProfileId = userAccountDto.BusinessProfileId,
            Address = userAccountDto.Address.ToEntity(),
            Contact = userAccountDto.Contact.ToEntity(),

        };

        return userAccount;
    }

    public static UserProfileDto ToDto(this UserProfile userAccountEntity)
    {
      
        if (userAccountEntity.Address is null)
            userAccountEntity.Address = AddressMapper.Incomplete().ToEntity();

        if (userAccountEntity.Contact is null)
            userAccountEntity.Contact = ContactMapper.Incomplete().ToEntity();


        UserProfileDto userAccount = new()
        {
            Id = userAccountEntity.Id,
            UserAccountId = userAccountEntity.UserAccountId,
            Deleted = userAccountEntity.Deleted,
            Registered = userAccountEntity.Registered,
            BusinessProfileId = userAccountEntity.BusinessProfileId,
            Address = userAccountEntity.Address.ToDto(),
            Contact = userAccountEntity.Contact.ToDto(),

        };

        return userAccount;
    }
    public static UserProfile ToUpdate(this UserProfileDto dto, UserProfile db)
    {
      
        if (dto.Address is null)
            dto.Address = AddressMapper.Incomplete();

        if (dto.Contact is null)
            dto.Contact = ContactMapper.Incomplete();

        if (db.Address is null)
            db.Address = AddressMapper.Incomplete().ToEntity();

        if (db.Contact is null)
            db.Contact = ContactMapper.Incomplete().ToEntity();

            db.Id = dto.Id;
            db.UserAccountId = dto.UserAccountId;
            db.Deleted = dto.Deleted;
            db.Registered = dto.Registered;
            db.BusinessProfileId = dto.BusinessProfileId;
            db.Address = dto.Address.ToEntity();
            db.Contact = dto.Contact.ToEntity();

        return db;
    }

    public static UserProfileDto Incomplete()
    {
        UserProfileDto incomplete = new()
        {
            Id = 0,
            Deleted = DateTime.MinValue,
            Registered = DateTime.Now,
             UserAccountId = "Cadastro Incompleto",
            
            
            BusinessProfileId = 0,
            Address = AddressMapper.Incomplete(),
            Contact = ContactMapper.Incomplete(),
         
        };

        return incomplete;
    }


}