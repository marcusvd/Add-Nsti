using System;
using System.Threading.Tasks;


using UnitOfWork.Persistence.Operations;
using Application.Exceptions;
using Domain.Entities.System.Customers;
using Application.Customers.Dtos;

using System.Net;
// using Application.Customers.Dtos.Mappers;

namespace Application.Customers
{
    public class CustomerAddServices : ICustomerAddServices
    {
        // private readonly ICustomerObjectMapperServices _ICustomerObjectMapperServices;
        private readonly IUnitOfWork _genericRepo;
        public CustomerAddServices(
                         IUnitOfWork genericRepo
                        //  ICustomerObjectMapperServices ICustomerObjectMapperServices
                        )
        {
            // _ICustomerObjectMapperServices = ICustomerObjectMapperServices;
            _genericRepo = genericRepo;
        }

        public async Task<HttpStatusCode> AddAsync(CustomerDto dtoView)
        {
            if (dtoView == null) throw new GlobalServicesException(GlobalErrorsMessagesException.IsObjNull);

            Customer entityToDb = new(){Name = "", TradeName = "", CNPJ = ""};
            // Customer entityToDb = _ICustomerObjectMapperServices.CustomerMapper(dtoView);

            entityToDb.Registered = DateTime.Now;

            _genericRepo.Customers.Add(entityToDb);

            if (await _genericRepo.Save())
                return HttpStatusCode.Created;

            throw new GlobalServicesException(GlobalErrorsMessagesException.UnknownError);
        }

    }
}