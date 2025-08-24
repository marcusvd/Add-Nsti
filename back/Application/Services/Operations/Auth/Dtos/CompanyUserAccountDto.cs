
using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;


public class CompanyUserAccountDto : RootBaseDto
{
    public int CompanyAuthId { get; set; }
    public CompanyAuthDto? CompanyAuth { get; set; }

    public int UserAccountId { get; set; }
    public UserAccountDto? UserAccount { get; set; }

    public DateTime LinkedOn { get; set; } = DateTime.UtcNow;
}

