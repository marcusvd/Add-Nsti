using UnitOfWork.Persistence.Operations;
using Application.Shared.Dtos;
using Application.Exceptions;
using Application.Helpers.Inject;


namespace Application.Shared.Operations;

public class ContactServices : IContactServices
{

    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;
    public ContactServices(
                     IUnitOfWork genericRepo,
                     IValidatorsInject validatorsInject
                    )
    {

        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
    }

    public async Task<bool> AddAsync(ContactDto contactDto)
    {
        _validatorsInject.GenericValidators.IsObjNull(contactDto);

        _genericRepo.Contacts.Add(contactDto);

        return await _genericRepo.Save();
    }

    public async Task<bool> UpdateAsync(ContactDto contactDto, int id)
    {
        _validatorsInject.GenericValidators.IsObjNull(contactDto);
        _validatorsInject.GenericValidators.Validate(contactDto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        var fromDb = await _genericRepo.Contacts.GetByPredicate(
           x => x.Id == id && x.Deleted == DateTime.MinValue,
           null,
           selector => selector,
           null
        );

        var toUpdate = contactDto.ToUpdate(fromDb);

        _genericRepo.Contacts.Update(toUpdate);

        return await _genericRepo.Save();
    }
}
