namespace Application.Services.Operations.Auth.Dtos;

public class UserAccountDto
{
    public int BusinessAuthId { get; set; }
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
    // public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();
}