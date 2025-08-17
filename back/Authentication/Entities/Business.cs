namespace Authentication.Entities;

public class Business
{
    public required int Id { get; set; }
    public required string Name { get; set; } = "Group business";
    public ICollection<UserAccount> UsersAccounts { get; set; }  = new List<UserAccount>();
    public ICollection<CompanyAuth> Companies { get; set; } = new List<CompanyAuth>();
}