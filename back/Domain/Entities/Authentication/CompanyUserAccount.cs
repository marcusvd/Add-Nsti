
namespace Domain.Entities.Authentication;


public class CompanyUserAccount : RootBaseAuth
{
    public int CompanyAuthId { get; set; }
    public CompanyAuth? CompanyAuth { get; set; }

    public int UserAccountId { get; set; }
    public UserAccount? UserAccount { get; set; }

    public DateTime LinkedOn { get; set; } = DateTime.UtcNow;
}

