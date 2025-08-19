namespace Authentication.Entities;

public class UserToken
{
    public int Id { get; set; }
    public int BusinessId { get; set; }
    public bool Authenticated { get; set; }
    public DateTime Expiration { get; set; }
    public string? Token { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    // public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();
    public string? Action { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}