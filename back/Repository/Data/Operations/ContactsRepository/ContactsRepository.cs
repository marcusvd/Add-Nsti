using Domain.Entities.Shared;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;
using Repository.Data.PersonalData.Contracts;

namespace Repository.Data.Operations.ContactsRepository;

    public class ContactsRepository : Repository<Contact>, IContactsRepository
    {
        private readonly ImSystemDbContext _CONTEXT;
        public ContactsRepository(ImSystemDbContext CONTEXT) : base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }

      

    }