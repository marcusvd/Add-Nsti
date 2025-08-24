using Domain.Entities.Shared;

namespace Domain.Entities.System.Profiles;

public class UserProfile : RootBase
{
    public required string UserAccountId { get; set; }
    public virtual Address? Address { get; set; }
    public virtual Contact? Contact { get; set; }
    // public string UserName { get; set; }
    // public string Email { get; set; }
    // public virtual Company Company { get; set; }
    // public DateTime Deleted { get; set; }
    // public DateTime Registered { get; set; } = DateTime.UtcNow;
}