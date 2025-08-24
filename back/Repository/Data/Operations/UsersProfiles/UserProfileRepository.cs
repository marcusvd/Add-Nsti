
using Domain.Entities.System.Profiles;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Companies
{
    public class UserProfileRepository : Repository<UserProfile>,IUserProfileRepository
    {
        private  ImSystemDbContext _CONTEXT;
        public UserProfileRepository(ImSystemDbContext CONTEXT):base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }
    }
}