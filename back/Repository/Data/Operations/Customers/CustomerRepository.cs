using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagination.Models;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;
using Repository.Helpers;
using System.Collections.Generic;
using Domain.Entities.System.Customers;

namespace Repository.Data.Operations.Main.Customers
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ImSystemDbContext _CONTEXT;

        public CustomerRepository(ImSystemDbContext CONTEXT) : base(CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }

          public async void AddRangeAsync(List<Customer> entities)
        {
            await _CONTEXT.MN_Customers.AddRangeAsync(entities);
        }
       
    }

}
