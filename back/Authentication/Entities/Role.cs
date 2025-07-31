using Microsoft.AspNetCore.Identity;


namespace Authentication.Entities;

public class Role : IdentityRole<int>
{
    public List<UserRole>? UserRoles { get; set; }
}