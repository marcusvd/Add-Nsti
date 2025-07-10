using Domain.Entities.Shared;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;
using Repository.Data.PersonalData.Contracts;

namespace Repository.Data.PersonalData.Operations
{
    public class AddressesRepository : Repository<Address>, IAddressesRepository
    {
        private readonly ImSystemDbContext _CONTEXT;
        public AddressesRepository(ImSystemDbContext CONTEXT) : base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }

    }
}