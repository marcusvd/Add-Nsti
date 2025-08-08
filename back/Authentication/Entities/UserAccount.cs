using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities;

public class UserAccount : IdentityUser<int>
{
    public int CompanyId { get; set; }
    public virtual int AddressId { get; set; }
    public virtual int ContactId { get; set; }
    public string Group { get; set; } = "User";
    public bool Deleted { get; set; }
    public DateTime Registered { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
}