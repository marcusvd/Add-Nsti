using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Authentication;

public class UserAccountClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserAccount>
{
    public UserAccountClaimsPrincipalFactory(UserManager<UserAccount> userManager,
    IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    { }
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserAccount user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("User", user.Group));
        return identity;
    }
}