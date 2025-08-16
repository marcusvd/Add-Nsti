export class UserTokenDto {
  authenticated!: boolean;
  expiration!: Date;
  token!: string;
  id!: number;
  userName!: string;
  email!: string;
  companyUserAccounts!: string[];
  action!: string;
  Roles!: string[];
}
    // public bool Authenticated { get; set; }
    // public DateTime Expiration { get; set; }
    // public string? Token { get; set; }
    // public int Id { get; set; }
    // public string? UserName { get; set; }
    // public string? Email { get; set; }
    // public ICollection<CompanyUserAccount> CompanyUserAccounts { get; set; } = new List<CompanyUserAccount>();
    // public string? Action { get; set; }
    // public IList<string> Roles { get; set; } = new List<string>();
