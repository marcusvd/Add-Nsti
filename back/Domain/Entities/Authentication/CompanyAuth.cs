namespace Domain.Entities.Authentication;

public class CompanyAuth : RootBaseAuth
{
    public int Id { get; set; }
    public required string CompanyProfileId { get; set; }
    public required string Name { get; set; }
    public required string TradeName { get; set; }
    public int BusinessId { get; set; }
    public BusinessAuth? Business { get; set; }
    public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();
}