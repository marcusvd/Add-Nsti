using System;
using Domain.Entities.Companies;
using Domain.Entities.Shared;

namespace Domain.Entities.Profiles;

public class MyUser
{
    public int UserAccoutnId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public virtual Company Company { get; set; }
    public virtual Address Address { get; set; }
    public virtual Contact Contact { get; set; }
    public Profile Profile { get; set; }
    public bool Deleted { get; set; }
    public DateTime Registered { get; set; } = DateTime.UtcNow;
}