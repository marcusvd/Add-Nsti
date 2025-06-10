using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Operations.Main.Customers.Dtos;
using Pagination.Models;

namespace Application.Services.Operations.Main.Customers
{
    public interface ICustomerGetServices
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAllIncluded(int customerId);
        Task<List<CustomerDto>> GetAllByCompanyIdAsync(int id);
        Task<PagedList<CustomerDto>> GetAllPagedAsync(Params parameters);
        Task<PagedList<CustomerDto>> GetAllCustomersByTermSearchPagedAsync(Params parameters);
        Task<CustomerDto> GetByIdIncludedPhysicallyMovingCosts(int customerId);
        Task<List<CustomerDto>> GetByCompanyIdIncludedPhysicallyMovingCosts(int companyId);
    }
}