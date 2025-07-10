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
        public Company Company { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public string Group { get; set; } = "User";
        public bool Deleted { get; set; }
        public DateTime Registered { get; set; }
        public List<UserRole> UserRoles { get; set; }

    }
}