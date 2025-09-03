using Application.Services.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Dtos;

public class UserAccountDto : RootBaseDto
{
    public int BusinessAuthId { get; set; }
    public required string UserName { get; set; }
    public required string UserProfileId { get; set; }
    public BusinessAuthDto BusinessAuth { get; set; } = BusinessAuthMapper.Incomplete();
    public DateTime? LastLogin { get; set; }
    public string? RefreshToken { get; set; }
    public required string DisplayUserName { get; set; }
    public required string Email { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    // public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<CompanyUserAccountDto> CompanyUserAccounts { get; set; } = new List<CompanyUserAccountDto>();
}

public static class UserAccountMapper
{
    public static UserAccount ToEntity(this UserAccountDto dto)
    {
        UserAccount userAccount = new()
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            BusinessAuthId = dto.BusinessAuthId,
            UserName = dto.UserName,
            UserProfileId = dto.UserProfileId,
            LastLogin = dto.LastLogin,
            RefreshToken = dto.RefreshToken,
            DisplayUserName = dto.DisplayUserName,
            Email = dto.Email,
            RefreshTokenExpiryTime = dto.RefreshTokenExpiryTime,


        };

        return userAccount;
    }

    public static UserAccountDto ToDto(this UserAccount entity)
    {
        UserAccountDto userAccount = new()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            Registered = entity.Registered,
            BusinessAuthId = entity.BusinessAuthId,
            UserName = entity.UserName ?? "Cadastro Incompleto",
            UserProfileId = entity.UserProfileId,
            LastLogin = entity.LastLogin,
            RefreshToken = entity.RefreshToken,
            DisplayUserName = entity.DisplayUserName,
            Email = entity.Email,
            RefreshTokenExpiryTime = entity.RefreshTokenExpiryTime,

        };
        return userAccount;
    }

    public static UserAccountDto Incomplete()
    {
        UserAccountDto incomplete = new()
        {
            Id = 0,
            Deleted = DateTime.MinValue,
            Registered = DateTime.Now,
            BusinessAuthId = 0,
            UserName = "Cadastro Incompleto",
            UserProfileId = "Cadastro Incompleto",
            LastLogin = DateTime.Now,
            RefreshToken = "Cadastro Incompleto",
            DisplayUserName = "Cadastro Incompleto",
            Email = "Cadastro Incompleto",
            RefreshTokenExpiryTime = DateTime.Now,

        };

        return incomplete;
    }


}