using Domain.Entities.Companies;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Companies
{
    public interface ICompanyRepository: IRepository<Company>
    { }
}