using Authentication.Entities;
using Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.AuthenticationRepository.UserAccountRepository;

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
                UserName = "Invalid",
                DisplayUserName = "Invalid@Invalid.com.br",
                Email = "Invalid@Invalid.com.br"
            };

        return userAny;
    }



}