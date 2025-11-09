using Domain.Entities.Authentication;
using Application.Auth.Dtos;
using Application.UsersAccountsServices.Dtos.Roles;
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Dtos;
using Application.Auth.UsersAccountsServices.Dtos.Extends;
using Application.BusinessesServices.Dtos.Auth;
using Domain.Entities.System.Businesses;

namespace Application.UsersAccountsServices.Dtos;

public class UserAccountDto : UserAccountBaseDto
{
    public int BusinessAuthId { get; set; }
    public required string UserName { get; set; }
    public required string UserProfileId { get; set; }
    public BusinessAuthDto? BusinessAuth { get; set; }
    // public BusinessAuthDto BusinessAuth { get; set; } = (BusinessAuthDto)GenericValidators.ReplaceNullObj<BusinessAuthDto>();
    public DateTime LastLogin { get; set; }
    public DateTime Code2FaSendEmail { get; set; }
    public TimedAccessControlDto TimedAccessControl { get; set; } = new TimedAccessControlDto();
    public DateTime WillExpire { get; set; }
    public string? RefreshToken { get; set; }
    public required string DisplayUserName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    public ICollection<CompanyUserAccountDto> CompanyUserAccounts { get; set; } = new List<CompanyUserAccountDto>();


    public static implicit operator UserAccount(UserAccountDto dto)
    {
        return new()
        {
            Id = dto.Id,
            UserProfileId = dto.UserProfileId,
            UserName = dto.UserName,
            BusinessAuthId = dto.BusinessAuthId,
            BusinessAuth = dto.BusinessAuth ?? new BusinessAuth() { BusinessProfileId = "placeholder", Name = "placeholder" },
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            LastLogin = dto.LastLogin,
            Code2FaSendEmail = dto.Code2FaSendEmail,
            EmailConfirmed = dto.EmailConfirmed,
            TimedAccessControl = dto.TimedAccessControl ?? new TimedAccessControl(),
            WillExpire = dto.WillExpire,
            RefreshToken = dto.RefreshToken,
            DisplayUserName = dto.DisplayUserName,
            Email = dto.Email,
            RefreshTokenExpiryTime = dto.RefreshTokenExpiryTime,
            // UserRoles = dto.UserRoles.Select(x => (UserRole)x).ToList(),
        };
    }
    public static implicit operator UserAccountDto(UserAccount dto)
    {
        return new()
        {
            Id = dto.Id,
            UserProfileId = dto.UserProfileId,
            UserName = dto.UserName,
            BusinessAuthId = dto.BusinessAuthId,
            BusinessAuth = dto.BusinessAuth ?? new BusinessAuth() { BusinessProfileId = "placeholder", Name = "placeholder" },
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            LastLogin = dto.LastLogin,
            Code2FaSendEmail = dto.Code2FaSendEmail,
            EmailConfirmed = dto.EmailConfirmed,
            TimedAccessControl = dto.TimedAccessControl ?? new TimedAccessControlDto(),
            WillExpire = dto.WillExpire,
            RefreshToken = dto.RefreshToken,
            DisplayUserName = dto.DisplayUserName,
            Email = dto.Email,
            RefreshTokenExpiryTime = dto.RefreshTokenExpiryTime,
            // UserRoles = dto.UserRoles.Select(x => (UserRoleDto)x).ToList(),
        };
    }


}

public static class UserAccountAuthExtensions
{
    public static UserAccount ToUpdate(this UserAccountDto dto, UserAccount db)
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