using System.Threading.Tasks;

using Repository.Data.PersonalData.Contracts;
using Repository.Data.Operations.Main.Customers;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.BusinessesProfiles;
using Authentication.AuthenticationRepository.BusinessRepository;
using Authentication.AuthenticationRepository.BusinessAuthRepository;
using Authentication.AuthenticationRepository.UserAccountRepository;

namespace UnitOfWork.Persistence.Operations
{
    public interface IUnitOfWork
    {
        #region USER
        IUserProfileRepository UsersProfiles { get; }
        IUserAccountRepository UsersAccounts { get; }
        #endregion

        #region BUSINESS
        IBusinessesProfilesRepository BusinessesProfiles { get; }
        IBusinessAuthRepository BusinessesAuth { get; }
        #endregion

        #region CUSTOMER
        ICustomerRepository Customers { get; }
        #endregion

        #region COMPANIES
        ICompanyProfileRepository CompaniesProfile { get; }
        ICompanyAuthRepository CompaniesAuth { get; }
        #endregion

        #region COMPANY_USERACCOUNT
        ICompanyAuthUserAccountRepository CompaniesUserAccounts { get; }
        #endregion

        #region ADDRESSES
        IAddressesRepository Addresses { get; }
        #endregion

        #region CONTACTS
        IContactsRepository Contacts { get; }
        #endregion
        Task<bool> Save();
        Task<bool> SaveID();
    }
}

