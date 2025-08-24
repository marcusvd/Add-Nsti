using System.Threading.Tasks;

using Repository.Data.PersonalData.Contracts;
using Repository.Data.Operations.Main.Customers;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.BusinessesProfiles;

namespace UnitOfWork.Persistence.Operations
{
    public interface IUnitOfWork
    {
        #region USERPROFILE
        IUserProfileRepository UsersProfiles { get; }
        #endregion      
        #region USERPROFILE
        IBusinessesProfilesRepository BusinessesProfiles { get; }
        #endregion      
        #region CUSTOMER
        ICustomerRepository Customers { get; }
        #endregion      
        #region COMPANIES
        ICompanyProfileRepository CompaniesProfile { get; }
        #endregion
        #region ADDRESSES
        IAddressesRepository Addresses { get; }
        #endregion
        #region CONTACTS
        IContactsRepository Contacts { get; }
        #endregion
        Task<bool> save();
    }
}

