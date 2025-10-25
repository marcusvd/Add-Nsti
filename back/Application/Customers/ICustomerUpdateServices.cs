using System.Net;
using System.Threading.Tasks;
using Application.Customers.Dtos;

namespace Application.Customers
{
    public interface ICustomerUpdateServices
    {
       Task<HttpStatusCode> UpdateAsync(int customerId, CustomerDto entity);
       Task<HttpStatusCode> DeleteFakeAsync(int customerId);
        
    }
}