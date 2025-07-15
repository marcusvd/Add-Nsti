using Domain.Entities.Shared;
using Domain.Entities.Companies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Authentication
{
    public class MyUser : IdentityUser<int>
    {

        public int CompanyId { get; set; }
        public virtual int AddressId { get; set; }
        public virtual int ContactId { get; set; }
        public string Group { get; set; } = "User";
        public bool Deleted { get; set; }
        public DateTime Registered { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        // public int CompanyId { get; set; }
        // public virtual Company Company { get; set; }
        // public virtual Address Address { get; set; }
        // public virtual Contact Contact { get; set; }
        // public string Group { get; set; } = "User";
        // public bool Deleted { get; set; }
        // public DateTime Registered { get; set; } = DateTime.UtcNow;
        // public DateTime? LastLogin { get; set; }
        // public string RefreshToken { get; set; }
        // public DateTime RefreshTokenExpiryTime { get; set; }
        // public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}