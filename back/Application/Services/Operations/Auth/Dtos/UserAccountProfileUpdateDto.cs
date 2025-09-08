// using Application.Services.Shared.Dtos;
// using Domain.Entities.System.Profiles;

// namespace Application.Services.Operations.Auth.Dtos;

// public class UserAccountProfileUpdateDto : RootBaseDto
// {
//     public  string? UserAccountId { get; set; }
//     public int BusinessProfileId { get; set; }
//     public required AddressDto Address { get; set; }
//     public required ContactDto Contact { get; set; }
// }

// public static class UserAccountProfileUpdateDtoMapper
// {
//     public static UserProfile ToUpdate(this UserAccountProfileUpdateDto dto, UserProfile db)
//     {

//         db.Id = dto.Id;
//         db.Deleted = dto.Deleted;
//         db.Registered = dto.Registered;
//         db.BusinessProfileId = dto.BusinessProfileId;
//         db.UserAccountId = dto.UserAccountId;
//         db.Address = dto.Address.ToEntity();
//         db.Contact = dto.Contact.ToEntity();
//         return db;
//     }


// }