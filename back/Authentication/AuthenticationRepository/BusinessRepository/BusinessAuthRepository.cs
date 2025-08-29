using Domain.Entities.Authentication;
using Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.AuthenticationRepository.BusinessAuthRepository;

public class BusinessAuthRepository : AuthRepository<BusinessAuth>, IBusinessAuthRepository
{

    private readonly IdImDbContext _dbContext;

    public BusinessAuthRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    // public async Task<BusinessAuth> BusinessFullAsync(int id)
    // {

    //     var businessGroup = await GetByPredicate(
    //      x => x.Id == id,
    //      add =>
    //      add.Include(x => x.UsersAccounts)
    //     .Include(x => x.Companies),
    //      selector => selector,
    //      ordeBy => ordeBy.OrderBy(x => x.Name)
    //      );

    //     if (businessGroup == null)
    //         return new BusinessAuth
    //         {
    //             Id = -1,
    //             Name = "Invalid",
    //             BusinessProfileId = "-1"
    //         };

    //     return businessGroup;

    // }



}