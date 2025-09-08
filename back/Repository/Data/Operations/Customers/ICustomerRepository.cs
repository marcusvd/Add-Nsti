using System.Collections.Generic;
using Domain.Entities.System.Customers;
using Repository.Data.Operations.Repository;

namespace Repository.Data.Operations.Customers;

public interface ICustomerRepository : IRepository<Customer>
{
    void AddRangeAsync(List<Customer> entities);
}