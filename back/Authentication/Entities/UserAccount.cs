using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities;

public class UserAccount : IdentityUser<int>
{

    public virtual int AddressId { get; set; }
    public virtual int ContactId { get; set; }
    public int BusinessId { get; set; }
    public Business? Business { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
    public string? RefreshToken { get; set; }
    public required string DisplayUserName { get; set; }
    public override string Email { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();

}