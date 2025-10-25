using System.Net;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Application.Customers.Dtos;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.System.Customers;
using Repository.Data.Operations.Customers;

namespace Application.Customers;

public class CustomerUpdateServices : ICustomerUpdateServices
{

    private readonly ICustomerRepository _iCustomerRepository;
    private readonly IUnitOfWork _genericRepo;


    public CustomerUpdateServices(
        ICustomerRepository ICustomerRepository,
        IUnitOfWork genericRepo
        )
    {
        _iCustomerRepository = ICustomerRepository;
        _genericRepo = genericRepo;
    }

    public async Task<HttpStatusCode> UpdateAsync(int customerId, CustomerDto entity)
    {
        if (entity == null) throw new GlobalServicesException(GlobalErrorsMessagesException.IsObjNull);
        if (customerId != entity.Id) throw new GlobalServicesException(GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        var fromDb = await _iCustomerRepository.GetByPredicate(
            x => x.Id == customerId,
            null,
            selector => selector
            );

        // var updated = _ICustomerObjectMapperServices.CustomerUpdateMapper(entity, fromDb);

        var updated = new Customer(){Name = "", TradeName = "", CNPJ = ""};

        _genericRepo.Customers.Update(updated);

        var result = await _genericRepo.Save();

        if (result)
            return HttpStatusCode.OK;

        return HttpStatusCode.BadRequest;
    }
    public async Task<HttpStatusCode> DeleteFakeAsync(int customerId)
    {

        var fromDb = await _iCustomerRepository.GetByPredicate(
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

        _genericRepo.Customers.Update(fromDb);

        var result = await _genericRepo.Save();

        if (result)
            return HttpStatusCode.OK;

        return HttpStatusCode.BadRequest;
    }
}