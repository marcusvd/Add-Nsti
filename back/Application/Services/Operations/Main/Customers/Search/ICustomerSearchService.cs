using System.Net;
using System.Threading.Tasks;
using Application.Services.Operations.Main.Customers.Dtos;
using Domain.Entities.Main.Customers;
using Pagination.Models;

namespace Application.Services.Operations.Main.Customers.Search
{
    public interface ICustomerSearchService
    {
         Task<Page<Customer>> FilterList(Params parameters, FilterTerms filterTerms);
    }
}