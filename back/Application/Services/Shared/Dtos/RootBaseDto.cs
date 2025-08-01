
using System;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Auth.Dtos;

namespace Application.Services.Shared.Dtos
{
    public abstract class RootBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserAccountDto User { get; set; }
        public int CompanyId { get; set; }
        public CompanyDto Company { get; set; }
        public DateTime Deleted { get; set; }
        public DateTime Registered { get; set; }
    }

}