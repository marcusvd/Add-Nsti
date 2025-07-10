using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Domain.Entities.Authentication
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<MyUser>
    {
        public MyUserClaimsPrincipalFactory(UserManager<MyUser> userManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(MyUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("User", user.Group));
            return identity;
        }
    }
}