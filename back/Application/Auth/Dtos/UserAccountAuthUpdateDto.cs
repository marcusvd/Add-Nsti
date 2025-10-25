using Application.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.Dtos;

public class UserAccountAuthUpdateDto : RootBaseDto
{
    public int BusinessAuthId { get; set; }
    public required string UserName { get; set; }
    public required string UserProfileId { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime WillExpire { get; set; }
    public string? RefreshToken { get; set; }
    public required string DisplayUserName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }   
    public ICollection<CompanyUserAccountDto> CompanyUserAccounts { get; set; } = new List<CompanyUserAccountDto>();
}

// public static class UserAccountAuthUpdateDtoMapper
// {
//     public static UserAccount ToUpdate(this UserAccountAuthUpdateDto dto, UserAccount db)
//     {
     
//            db.Id = dto.Id;
//            db.Deleted = dto.Deleted;
//            db.Registered = dto.Registered;
//            db.BusinessAuthId = dto.BusinessAuthId;
//            db.UserName = dto.UserName;
//            db.UserProfileId = dto.UserProfileId;
//            db.LastLogin = dto.LastLogin;
//            db.WillExpire = dto.WillExpire;
//            db.RefreshToken = dto.RefreshToken;
//            db.DisplayUserName = dto.DisplayUserName;
//            db.RefreshTokenExpiryTime = dto.RefreshTokenExpiryTime;

//         return db;
//     }


// }