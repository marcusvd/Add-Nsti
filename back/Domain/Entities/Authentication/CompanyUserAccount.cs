using Domain.Entities.System.Companies;

namespace Domain.Entities.Authentication;


public class CompanyUserAccount 
{
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
    public int CompanyAuthId { get; set; }
    public CompanyAuth? CompanyAuth { get; set; }
    public int UserAccountId { get; set; }
    public UserAccount? UserAccount { get; set; }
    public DateTime LinkedOn { get; set; } = DateTime.UtcNow;
}

