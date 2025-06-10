using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities.Authentication
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles {get; set;}
    }
}