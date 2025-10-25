

using Domain.Entities.System.Businesses;
using Domain.Entities.System.Profiles;
using Repository.Data.Context;
using Repository.Data.Operations.BusinessesProfiles;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Companies;
    public class BusinessesProfilesRepository : Repository<BusinessProfile>,IBusinessesProfilesRepository
    {
        private  ImSystemDbContext _CONTEXT;
        public BusinessesProfilesRepository(ImSystemDbContext CONTEXT):base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }
    }
