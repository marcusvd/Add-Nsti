using Application.Shared.Dtos;
using Application.UsersAccountsServices.Dtos;
using Domain.Entities.Authentication;
using Application.Auth.UsersAccountsServices.Dtos;
using Domain.Entities.System.Profiles;
using Domain.Entities.Shared;
using Application.Shared.Validators;

namespace Application.UsersAccountsServices.Dtos.Mappers;

public static class UserAccountMappers
{
    public static UserProfile ToUpdate(this UserProfileDto dto, UserProfile db)
    {

        if (dto.Address is null)
            dto.Address = new AddressDto();

        if (dto.Contact is null)
            dto.Contact = new ContactDto();

        if (db.Address is null)
            db.Address = (Address)GenericValidators.ReplaceNullObj<Address>();

        if (db.Contact is null)
            db.Contact = new ContactDto();

        db.Id = dto.Id;
        db.UserAccountId = dto.UserAccountId;
        db.Deleted = dto.Deleted;
        db.Registered = dto.Registered;
        db.BusinessProfileId = dto.BusinessProfileId;
        db.Address = dto.Address;
        db.Contact = dto.Contact;

        return db;
    }
    public static UserAccount ToUpdate(this UserAccount db, UserAccountDto dto)
    {

        db.Id = dto.Id;
        db.Deleted = dto.Deleted;
        db.Registered = dto.Registered;
        db.BusinessAuthId = dto.BusinessAuthId;
        db.UserName = dto.UserName;
        db.UserProfileId = dto.UserProfileId;
        db.LastLogin = dto.LastLogin;
        db.WillExpire = dto.WillExpire;
        db.RefreshToken = dto.RefreshToken;
        db.DisplayUserName = dto.DisplayUserName;
        db.RefreshTokenExpiryTime = dto.RefreshTokenExpiryTime;

        return db;
    }

}