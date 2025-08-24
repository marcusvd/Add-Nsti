using System.Threading.Tasks;
using Repository.Data.Context;


using Repository.Data.PersonalData.Contracts;
using Repository.Data.PersonalData.Operations;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.Main.Customers;
using Repository.Data.Operations.BusinessesProfiles;

namespace UnitOfWork.Persistence.Operations
{
    public class Worker : IUnitOfWork
    {
        private readonly ImSystemDbContext _CONTEXT;
        public Worker(ImSystemDbContext CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }

        #region USER_PROFILE
        private BusinessesProfilesRepository _BUSINESS_PROFILE_REPO;
        public IBusinessesProfilesRepository BusinessesProfiles
        {
            get
            {
                return _BUSINESS_PROFILE_REPO = _BUSINESS_PROFILE_REPO ?? new BusinessesProfilesRepository(_CONTEXT);
            }
        }
        #endregion      
        #region USER_PROFILE
        private UserProfileRepository _USER_PROFILE_REPO;
        public IUserProfileRepository UsersProfiles
        {
            get
            {
                return _USER_PROFILE_REPO = _USER_PROFILE_REPO ?? new UserProfileRepository(_CONTEXT);
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
        

        public async Task<bool> save()
        {
            return await _CONTEXT.SaveChangesAsync() > 0;
        }
    }
}
