using System.Threading.Tasks;
using Repository.Data.Context;


using Repository.Data.PersonalData.Contracts;
using Repository.Data.PersonalData.Operations;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.Main.Customers;
using Repository.Data.Operations.BusinessesProfiles;
using Authentication.AuthenticationRepository.BusinessRepository;
using Authentication.Context;
using Authentication.AuthenticationRepository.BusinessAuthRepository;
using Authentication.AuthenticationRepository.UserAccountRepository;

namespace UnitOfWork.Persistence.Operations
{
    public class Worker : IUnitOfWork
    {
        private readonly ImSystemDbContext _CONTEXT;
        private readonly IdImDbContext _ID_CONTEXT;
        public Worker(ImSystemDbContext CONTEXT, IdImDbContext ID_CONTEXT)
        {
            _CONTEXT = CONTEXT;
            _ID_CONTEXT = ID_CONTEXT;
        }

        #region BUSINESSES
        private BusinessesProfilesRepository _BUSINESS_PROFILE_REPO;
        public IBusinessesProfilesRepository BusinessesProfiles
        {
            get
            {
                return _BUSINESS_PROFILE_REPO = _BUSINESS_PROFILE_REPO ?? new BusinessesProfilesRepository(_CONTEXT);
            }
        }
        private BusinessAuthRepository _BUSINESS_AUTH;
        public IBusinessAuthRepository BusinessesAuth
        {
            get
            {
                return _BUSINESS_AUTH = _BUSINESS_AUTH ?? new BusinessAuthRepository(_ID_CONTEXT);
            }
        }
        #endregion      
        #region USER
        private UserProfileRepository _USER_PROFILE_REPO;
        public IUserProfileRepository UsersProfiles
        {
            get
            {
                return _USER_PROFILE_REPO = _USER_PROFILE_REPO ?? new UserProfileRepository(_CONTEXT);
            }
        }
        private UserAccountRepository _USER_ACCOUNT_REPO;
        public IUserAccountRepository UsersAccounts
        {
            get
            {
                return _USER_ACCOUNT_REPO = _USER_ACCOUNT_REPO ?? new UserAccountRepository(_ID_CONTEXT);
            }
        }
        #endregion      
        #region CUSTOMER
        private CustomerRepository _CUSTOMER_REPO;
        public ICustomerRepository Customers
        {
            get
            {
                return _CUSTOMER_REPO = _CUSTOMER_REPO ?? new CustomerRepository(_CONTEXT);
            }
        }
        #endregion      
        #region COMPANIES
        private CompanyProfileRepository _COMPANIES_REPO;
        public ICompanyProfileRepository CompaniesProfile
        {
            get
            {
                return _COMPANIES_REPO = _COMPANIES_REPO ?? new CompanyProfileRepository(_CONTEXT);
            }
        }
        private CompanyAuthRepository _COMPANIES_AUTH;
        public ICompanyAuthRepository CompaniesAuth
        {
            get
            {
                return _COMPANIES_AUTH = _COMPANIES_AUTH ?? new CompanyAuthRepository(_ID_CONTEXT);
            }
        }
        #endregion
        #region COMPANY_USERACCOUNT
        
        private CompanyAuthUserAccountRepository _COMPANIES_USERACCOUNTS_REPO;
        public ICompanyAuthUserAccountRepository CompaniesUserAccounts
        {
            get
            {
                return _COMPANIES_USERACCOUNTS_REPO = _COMPANIES_USERACCOUNTS_REPO ?? new CompanyAuthUserAccountRepository(_ID_CONTEXT);
            }
        }
        #endregion
    
    
    
    
        #region ADDRESSES
        private AddressesRepository _ADDRESSES_REPO;
        public IAddressesRepository Addresses
        {
            get
            {
                return _ADDRESSES_REPO = _ADDRESSES_REPO ?? new AddressesRepository(_CONTEXT);
            }
        }
        #endregion
        #region CONTACTS
        private ContactsRepository _CONTACTS_REPO;
        public IContactsRepository Contacts
        {
            get
            {
                return _CONTACTS_REPO = _CONTACTS_REPO ?? new ContactsRepository(_CONTEXT);
            }
        }
        #endregion


        public async Task<bool> Save()
        {
            return await _CONTEXT.SaveChangesAsync() > 0;
        }
        public async Task<bool> SaveID()
        {
            return await _ID_CONTEXT.SaveChangesAsync() > 0;
        }
    }
}
