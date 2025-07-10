using System.Collections.Generic;
using Domain.Entities.Customers;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Main.Customers
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void AddRangeAsync(List<Customer> entities);
    }
}