using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Companies;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Companies
{
    public class CompanyRepository : Repository<Company>,ICompanyRepository
    {
        private  ImSystemDbContext _CONTEXT;
        public CompanyRepository(ImSystemDbContext CONTEXT):base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }
    }
}