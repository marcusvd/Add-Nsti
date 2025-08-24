using System.Net;
using System.Threading.Tasks;
using Application.Services.Operations.Customers.Dtos;
using Domain.Entities.System.Customers;
using Pagination.Models;

namespace Application.Services.Operations.Customers.Search
{
    public interface ICustomerSearchService
    {
         Task<Page<Customer>> FilterList(Params parameters, FilterTerms filterTerms);
    }
}