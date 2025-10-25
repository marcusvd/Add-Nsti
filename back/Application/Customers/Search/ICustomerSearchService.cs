using System.Net;
using System.Threading.Tasks;
using Application.Customers.Dtos;
using Domain.Entities.System.Customers;
using Pagination.Models;

namespace Application.Customers.Search
{
    public interface ICustomerSearchService
    {
         Task<Page<Customer>> FilterList(Params parameters, FilterTerms filterTerms);
    }
}