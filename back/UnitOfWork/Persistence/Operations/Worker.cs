using System.Threading.Tasks;
using Repository.Data.Context;
using Repository.Data.PersonalData.Contracts;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.BusinessesProfiles;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Context.Auth;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Repository.Data.Operations.Customers;
using Repository.Data.Operations.AddressRepository;
using Repository.Data.Operations.ContactsRepository;

namespace UnitOfWork.Persistence.Operations
{
  public class Worker : IUnitOfWork
  {
    private readonly ImSystemDbContext _context;
    private readonly IdImDbContext _id_Context;

    public Worker() { }


    public Worker(
                 ImSystemDbContext context,
                 IdImDbContext id_Context
                 )
    {
      _context = context;
      _id_Context = id_Context;
    }

 

    #region BUSINESSES
    private BusinessesProfilesRepository _business_ProfileRepo;
    public IBusinessesProfilesRepository BusinessesProfiles
    {
      get
      {
        return _business_ProfileRepo = _business_ProfileRepo ?? new BusinessesProfilesRepository(_context);
      }
    }
    private BusinessAuthRepository _business_Auth;
    public IBusinessAuthRepository BusinessesAuth
    {
      get
      {
        return _business_Auth = _business_Auth ?? new BusinessAuthRepository(_id_Context);
      }
    }
    #endregion
    #region USER
    private UserProfileRepository _userProfileRepo;
    public IUserProfileRepository UsersProfiles
    {
      get
      {
        return _userProfileRepo = _userProfileRepo ?? new UserProfileRepository(_context);
      }
    }
    private UserAccountRepository _userAccountRepo;
    public IUserAccountRepository UsersAccounts
    {
      get
      {
        return _userAccountRepo = _userAccountRepo ?? new UserAccountRepository(_id_Context);
      }
    }

    #endregion


    #region CUSTOMER
    private CustomerRepository _customerRepo;
    public ICustomerRepository Customers
    {
      get
      {
        return _customerRepo = _customerRepo ?? new CustomerRepository(_context);
      }
    }
    #endregion
    #region COMPANIES
    private CompanyProfileRepository _companiesRepo;
    public ICompanyProfileRepository CompaniesProfile
    {
      get
      {
        return _companiesRepo = _companiesRepo ?? new CompanyProfileRepository(_context);
      }
    }
    private CompanyAuthRepository _companiesAuth;
    public ICompanyAuthRepository CompaniesAuth
    {
      get
      {
        return _companiesAuth = _companiesAuth ?? new CompanyAuthRepository(_id_Context);
      }
    }
    #endregion
    #region COMPANY_USERACCOUNT

    private CompanyAuthUserAccountRepository _companiesUseraccountsRepo;
    public ICompanyAuthUserAccountRepository CompaniesUserAccounts
    {
      get
      {
        return _companiesUseraccountsRepo = _companiesUseraccountsRepo ?? new CompanyAuthUserAccountRepository(_id_Context);
      }
    }
    #endregion
    #region ADDRESSES
    private AddressRepository _addressesRepo;
    public IAddressRepository Addresses
    {
      get
      {
        return _addressesRepo = _addressesRepo ?? new AddressRepository(_context);
      }
    }
    #endregion
    #region CONTACTS
    private ContactsRepository _contactsRepo;
    public IContactsRepository Contacts
    {
      get
      {
        return _contactsRepo = _contactsRepo ?? new ContactsRepository(_context);
      }
    }
    #endregion



    private TimedAccessControlRepository _timedAccessControlRepo;
    public ITimedAccessControlRepository TimedAccessControls
    {
      get
      {
        return _timedAccessControlRepo = _timedAccessControlRepo ?? new TimedAccessControlRepository(_id_Context);
      }
    }

    public async Task<bool> Save()
    {
      return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> SaveID()
    {
      return await _id_Context.SaveChangesAsync() > 0;
    }
  }
}
