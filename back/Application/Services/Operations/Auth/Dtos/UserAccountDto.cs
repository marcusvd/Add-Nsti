namespace Application.Services.Operations.Auth.Dtos;

public class UserAccountDto
{
    // public UserAccountDto(int businessAuthId, DateTime deleted, DateTime registered, DateTime refreshTokenExpiryTime)
    // {
    //     this.BusinessAuthId = businessAuthId;
    //     this.Deleted = deleted;
    //     this.Registered = registered;
    //     this.RefreshTokenExpiryTime = refreshTokenExpiryTime;
    // }

    public int BusinessAuthId { get; set; }
    public required string UserName { get; set; }
    public required string UserProfileId { get; set; }
    public BusinessAuthDto? BusinessAuth { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
    public string? RefreshToken { get; set; }
    public required string DisplayUserName { get; set; }
    public required string Email { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    // public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<CompanyUserAccountDto> CompanyUserAccounts { get; set; } = new List<CompanyUserAccountDto>();
}