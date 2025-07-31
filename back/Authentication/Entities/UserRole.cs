using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities;

public class UserRole : IdentityUserRole<int>

{
    public UserAccount UserAccount { get; set; }
    public Role Role { get; set; }
}