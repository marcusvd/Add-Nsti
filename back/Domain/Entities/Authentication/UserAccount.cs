using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Authentication;

public class UserAccount : IdentityUser<int>
{
    public required string UserProfileId { get; set; }
    public int BusinessAuthId { get; set; }
    public BusinessAuth? BusinessAuth { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.Now;
    public DateTime LastLogin { get; set; }
    public DateTime Code2FaSendEmail { get; set; } = DateTime.MinValue;
    public TimedAccessControl? TimedAccessControl { get; set; } = new TimedAccessControl();
    public DateTime WillExpire { get; set; } = DateTime.MinValue;
    public string? RefreshToken { get; set; }
    public required string DisplayUserName { get; set; }
    public override string Email { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();

}