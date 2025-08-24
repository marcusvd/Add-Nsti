using Microsoft.AspNetCore.Identity;


namespace Domain.Entities.Authentication;

public class Role : IdentityRole<int>
{
    public List<UserRole>? UserRoles { get; set; }

    public required string DisplayRole { get; set; } = string.Empty;
    public override string ConcurrencyStamp { get; set; }  = Guid.NewGuid().ToString();
}