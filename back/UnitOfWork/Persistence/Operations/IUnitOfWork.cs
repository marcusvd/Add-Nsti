using System.Threading.Tasks;

using Repository.Data.PersonalData.Contracts;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.BusinessesProfiles;
using Authentication.Helpers;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Authentication;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Repository.Data.Operations.Customers;
using Repository.Data.Operations.AddressRepository;

namespace UnitOfWork.Persistence.Operations
{
    public interface IUnitOfWork
    {
        #region USER
        IUserProfileRepository UsersProfiles { get; }
        IUserAccountRepository UsersAccounts { get; }
        ITimedAccessControlRepository TimedAccessControls { get; }
        UserManager<UserAccount> UsersManager { get; }
        RoleManager<Role> RolesManager { get; }

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
        IAddressRepository Addresses { get; }
        #endregion

        #region CONTACTS
        IContactsRepository Contacts { get; }
        #endregion
        #region GENERIC_VALIDATOR
        IGenericValidatorServices _GenericValidatorServices { get; }
        #endregion

        Task<bool> Save();
        Task<bool> SaveID();
    }
}

