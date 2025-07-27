using System.Net;
using System.Threading.Tasks;
using Application.Services.Operations.Customers.Dtos;

namespace Application.Services.Operations.Customers
{
    public interface ICustomerAddServices
    {
         Task<HttpStatusCode> AddAsync(CustomerDto entity);
    }
}