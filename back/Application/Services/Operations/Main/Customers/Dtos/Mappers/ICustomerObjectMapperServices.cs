using System.Collections.Generic;
using Domain.Entities.Main;
using Domain.Entities.Main.Customers;


namespace Application.Services.Operations.Main.Customers.Dtos.Mappers
{
    public interface ICustomerObjectMapperServices
    {
        List<CustomerDto> CustomerListMake(List<Customer> list);
        List<Customer> CustomerListMake(List<CustomerDto> list);
        CustomerDto CustomerMapper(Customer entity);
        Customer CustomerMapper(CustomerDto entity);
        Customer CustomerUpdateMapper(CustomerDto dto, Customer db);
        
    }
}