using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


using Application.Exceptions;
using Application.Services.Operations.Customers.Dtos;
using Repository.Data.Operations.Main.Customers;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.System.Customers;
// using Application.Services.Operations.Customers.Dtos.Mappers;

namespace Application.Services.Operations.Customers;

public class CustomerUpdateServices : ICustomerUpdateServices
{

    private readonly ICustomerRepository _iCustomerRepository;
    // private readonly ICustomerObjectMapperServices _ICustomerObjectMapperServices;
    private readonly IUnitOfWork _GENERIC_REPO;


    public CustomerUpdateServices(
        ICustomerRepository ICustomerRepository,
        // ICustomerObjectMapperServices ICustomerObjectMapperServices,
        IUnitOfWork GENERIC_REPO
        )
    {
        _iCustomerRepository = ICustomerRepository;
        // _ICustomerObjectMapperServices = ICustomerObjectMapperServices;
        _GENERIC_REPO = GENERIC_REPO;
    }

    public async Task<HttpStatusCode> UpdateAsync(int customerId, CustomerDto entity)
    {
        if (entity == null) throw new GlobalServicesException(GlobalErrorsMessagesException.IsObjNull);
        if (customerId != entity.Id) throw new GlobalServicesException(GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        var fromDb = await _iCustomerRepository.GetById(
            x => x.Id == customerId,
            null,
            selector => selector
            );

        // var updated = _ICustomerObjectMapperServices.CustomerUpdateMapper(entity, fromDb);

        var updated = new Customer(){Name = "", TradeName = "", CNPJ = ""};

        _GENERIC_REPO.Customers.Update(updated);

        var result = await _GENERIC_REPO.save();

        if (result)
            return HttpStatusCode.OK;

        return HttpStatusCode.BadRequest;
    }
    public async Task<HttpStatusCode> DeleteFakeAsync(int customerId)
    {

        var fromDb = await _iCustomerRepository.GetById(
            x => x.Id == customerId,
            toInclude => toInclude.Include(x => x.Address)
            .Include(x => x.Contact)
            .ThenInclude(x => x.SocialMedias),
            selector => selector
            );

        fromDb.Deleted = DateTime.Now;

        if (fromDb.Contact != null)
            fromDb.Contact.Deleted = DateTime.Now;

        if (fromDb.Contact.SocialMedias != null)
            fromDb.Contact.SocialMedias.ToList().ForEach(x => { x.Deleted = DateTime.Now; });

        if (fromDb.Address != null)
            fromDb.Address.Deleted = DateTime.Now;

        _GENERIC_REPO.Customers.Update(fromDb);

        var result = await _GENERIC_REPO.save();

        if (result)
            return HttpStatusCode.OK;

        return HttpStatusCode.BadRequest;
    }
}