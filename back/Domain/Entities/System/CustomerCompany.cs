
using Domain.Entities.Shared;
using Domain.Entities.System.Companies;
using Domain.Entities.System.Customers;

namespace Domain.Entities.System;


public class CustomerCompany
{
    public int CompanyId { get; set; }
    public CompanyProfile? Company { get; set; }

    public int CustomerId { get; set; }
    public Customer? CUstomer { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
    public DateTime LinkedOn { get; set; } = DateTime.UtcNow;
}

