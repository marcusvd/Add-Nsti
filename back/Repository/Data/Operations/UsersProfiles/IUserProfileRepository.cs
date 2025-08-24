using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.System.Profiles;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Companies
{
    public interface IUserProfileRepository: IRepository<UserProfile>
    { }
}