namespace Domain.Entities.Authentication;

public class BusinessAuth : RootBaseAuth
{
    public required int Id { get; set; }
    public required string Name { get; set; } = "Group business";
    public required string BusinessProfileId { get; set; } 
    public ICollection<UserAccount> UsersAccounts { get; set; } = new List<UserAccount>();
    public ICollection<CompanyAuth> Companies { get; set; } = new List<CompanyAuth>();
}