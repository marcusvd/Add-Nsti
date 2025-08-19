using Authentication.Entities;
using Authentication.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.AuthenticationRepository.BusinessRepository;

public class BusinessRepository : AuthRepository<Business>, IBusinessRepository
{

    private readonly IdImDbContext _dbContext;

    public BusinessRepository(IdImDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Business> GetBusinessFull(int id)
    {

        var businessGroup = await GetByPredicate(
         x => x.Id == id,
         add =>
         add.Include(x => x.UsersAccounts)
        .Include(x => x.Companies),
         selector => selector,
         ordeBy => ordeBy.OrderBy(x => x.Name)
         );

        if (businessGroup == null)
            return new Business
            {
                Id = -1,
                Name = "Invalid"
            };

        return businessGroup;

    }



}