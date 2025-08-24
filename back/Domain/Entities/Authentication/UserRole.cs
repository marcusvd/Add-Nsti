using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Authentication;

public class UserRole : IdentityUserRole<int>

{
    public UserAccount UserAccount { get; set; }
    public Role Role { get; set; }
}