using UnitOfWork.Persistence.Operations;
using Application.Exceptions;
using Application.Shared.Dtos;
using Application.Helpers.Inject;


namespace Application.Shared.Operations;

public class AddressServices : IAddressServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;
    public AddressServices(
                     IUnitOfWork genericRepo,
                     IValidatorsInject validatorsInject
                    )
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
    }

    public async Task<bool> AddAsync(AddressDto addressDto)
    {
        _validatorsInject.GenericValidators.IsObjNull(addressDto);

        _genericRepo.Addresses.Add(addressDto);

        return await _genericRepo.Save();
    }

    public async Task<bool> UpdateAsync(AddressDto addressDto, int id)
    {
        _validatorsInject.GenericValidators.IsObjNull(addressDto);
        _validatorsInject.GenericValidators.Validate(addressDto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        var fromDb = await _genericRepo.Addresses.GetByPredicate(
           x => x.Id == id && x.Deleted == DateTime.MinValue,
           null,
           selector => selector,
           null
        );

        var toUpdate = addressDto.ToUpdate(fromDb);

        _genericRepo.Addresses.Update(toUpdate);

        return await _genericRepo.Save();
    }
}
