using System.Net;
using System.Threading.Tasks;
using Application.Services.Operations.Main.Customers.Dtos;

namespace Application.Services.Operations.Main.Customers
{
    public interface ICustomerUpdateServices
    {
       Task<HttpStatusCode> UpdateAsync(int customerId, CustomerDto entity);
       Task<HttpStatusCode> DeleteFakeAsync(int customerId);
        
    }
}