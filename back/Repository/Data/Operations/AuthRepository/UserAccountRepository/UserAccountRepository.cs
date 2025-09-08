using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Repository.Data.Context.Auth;

namespace Repository.Data.Operations.AuthRepository.UserAccountRepository;

public class UserAccountRepository : AuthRepository<UserAccount>, IUserAccountRepository
{

    private readonly IdImDbContext _dbContext;

    public UserAccountRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<UserAccount> GetUserAccountFull(string email)
    {

        var userAny = await GetByPredicate(
         x => x.Email.Equals(email),
         add => add.Include(x => x.CompanyUserAccounts),
         selector => selector,
         ordeBy => ordeBy.OrderBy(x => x.Email)
         );

        if (userAny == null)
            return new UserAccount
            {
                Id = -1,
                UserProfileId = "-1",
                UserName = "Invalid",
                DisplayUserName = "Invalid@Invalid.com.br",
                Email = "Invalid@Invalid.com.br"
            };

        return userAny;
    }



}