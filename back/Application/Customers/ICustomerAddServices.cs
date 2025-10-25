using System.Net;
using System.Threading.Tasks;
using Application.Customers.Dtos;

namespace Application.Customers
{
    public interface ICustomerAddServices
    {
         Task<HttpStatusCode> AddAsync(CustomerDto entity);
    }
}