
using Domain.Entities.System.Companies;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Companies
{
    public class CompanyProfileRepository : Repository<CompanyProfile>,ICompanyProfileRepository
    {
        private  ImSystemDbContext _CONTEXT;
        public CompanyProfileRepository(ImSystemDbContext CONTEXT):base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }
    }
}