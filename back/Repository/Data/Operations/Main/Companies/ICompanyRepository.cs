using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Entities.Main;
using Domain.Entities.Main.Companies;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Main.Companies
{
    public interface ICompanyRepository: IRepository<Company>
    {
        // Task<Company> GetByIdStockIncludedAsync(int id);
    }

    
}