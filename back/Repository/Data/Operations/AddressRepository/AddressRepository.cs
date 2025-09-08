
using Domain.Entities.Shared;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;


namespace Repository.Data.Operations.AddressRepository;
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly ImSystemDbContext _CONTEXT;

        public AddressRepository(ImSystemDbContext CONTEXT) : base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }
      
       
    }
