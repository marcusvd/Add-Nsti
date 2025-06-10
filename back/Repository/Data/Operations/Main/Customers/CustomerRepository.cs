using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Main.Customers;
using Pagination.Models;
using Repository.Data.Context;
using Repository.Data.Operations.Repository;
using Repository.Helpers;
using System.Collections.Generic;

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

        // public async Task<PagedList<Customer>> GetCustomersPagedAsync(Params parameters)
        // {

        //     IQueryable<Customer> query =
        //      GetAllPagination().OrderBy(x => x.Id)
        //      .Where(x => x.CompanyId == parameters.CompanyId);


        //     if (String.IsNullOrEmpty(parameters.Term))
        //     {
        //         return await PagedList<Customer>.ToPagedList(query, parameters.PgNumber, parameters.PgSize);
        //     }

        //     if (parameters.Term.Equals("null"))
        //     {
        //         return await PagedList<Customer>.ToPagedList(query, parameters.PgNumber, parameters.PgSize);
        //     }

        //     if (!string.IsNullOrEmpty(parameters.Term))
        //     {
        //         query = query.Where(p => p.XXX.Contains(parameters.Term.RemoveAccentsNormalize()));
        //     }


        //     return await PagedList<Customer>.ToPagedList(query, parameters.PgNumber, parameters.PgSize);
        // }

        // public async Task<Customer> GetByIdAIcludedPhysicallyMovingCostsAsync(int companyId, int customerId)
        // {
        //     var query = await _CONTEXT.MN_Customers.AsNoTracking().Where(x => x.CompanyId == companyId)
        //     .Include(x=>x.PhysicallyMovingCosts)
            
        //     .SingleOrDefaultAsync(x=> x.Id == customerId);
          
        //     return query;
        // }


    }

}
