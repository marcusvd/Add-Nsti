using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Main;
using Domain.Entities.Main.Companies;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data.Operations.Main.Companies
{
    public class CompanyRepository : Repository<Company>,ICompanyRepository
    {
        private  ImSystemDbContext _CONTEXT;
        public CompanyRepository(ImSystemDbContext CONTEXT):base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }

        // public async Task<Company> GetByIdStockIncludedAsync(int id)
        // {
        //    var query = await _CONTEXT.MN_Companies.AsNoTracking()
        //    .Include(x=> x.Stock).SingleOrDefaultAsync(x => x.Id == id);
        //    return query;
        // }
    }
}