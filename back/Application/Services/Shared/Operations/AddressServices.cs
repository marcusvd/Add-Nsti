using UnitOfWork.Persistence.Operations;
using Application.Exceptions;
using Application.Services.Shared.Dtos;


namespace Application.Services.Shared.Operations;

public class AddressServices : IAddressServices
{
    private readonly IUnitOfWork _GENERIC_REPO;
    public AddressServices(
                     IUnitOfWork GENERIC_REPO
                    )
    {
        _GENERIC_REPO = GENERIC_REPO;
    }

    public async Task<bool> AddAsync(AddressDto addressDto)
    {
        _GENERIC_REPO._GenericValidatorServices.IsObjNull(addressDto);

        var toDb = addressDto.ToEntity();

        _GENERIC_REPO.Addresses.Add(toDb);

        return await _GENERIC_REPO.Save();
    }

    public async Task<bool> UpdateAsync(AddressDto addressDto, int id)
    {
        _GENERIC_REPO._GenericValidatorServices.IsObjNull(addressDto);
        _GENERIC_REPO._GenericValidatorServices.Validate(addressDto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        var fromDb = await _GENERIC_REPO.Addresses.GetByPredicate(
           x => x.Id == id && x.Deleted == DateTime.MinValue,
           null,
           selector => selector,
           null
        );

        var toUpdate = addressDto.ToUpdate(fromDb);

        _GENERIC_REPO.Addresses.Update(toUpdate);

        return await _GENERIC_REPO.Save();
    }
}
