namespace Authentication.Entities;

public class CompanyAuth
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();
}