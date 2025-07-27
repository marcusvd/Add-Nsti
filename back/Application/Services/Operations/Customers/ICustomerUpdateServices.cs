using System.Net;
using System.Threading.Tasks;
using Application.Services.Operations.Customers.Dtos;

namespace Application.Services.Operations.Customers
{
    public interface ICustomerUpdateServices
    {
       Task<HttpStatusCode> UpdateAsync(int customerId, CustomerDto entity);
       Task<HttpStatusCode> DeleteFakeAsync(int customerId);
        
    }
}