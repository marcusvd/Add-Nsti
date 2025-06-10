using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Authentication
{
    public class UserRole : IdentityUserRole<int>
    
    {
        public MyUser MyUser { get; set; }
        public Role Role { get; set; }
    }
}