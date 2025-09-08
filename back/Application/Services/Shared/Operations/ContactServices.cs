using UnitOfWork.Persistence.Operations;
using Application.Services.Shared.Dtos;
using Application.Exceptions;


namespace Application.Services.Shared.Operations;

public class ContactServices : IContactServices
{

    private readonly IUnitOfWork _GENERIC_REPO;
    public ContactServices(
                     IUnitOfWork GENERIC_REPO

                    )
    {

        _GENERIC_REPO = GENERIC_REPO;
    }

    public async Task<bool> AddAsync(ContactDto ContactDto)
    {
        _GENERIC_REPO._GenericValidatorServices.IsObjNull(ContactDto);

        var toDb = ContactDto.ToEntity();

        _GENERIC_REPO.Contacts.Add(toDb);

        return await _GENERIC_REPO.Save(); 
    }

    public async Task<bool> UpdateAsync(ContactDto contactDto, int id)
    {
       _GENERIC_REPO._GenericValidatorServices.IsObjNull(contactDto);
       _GENERIC_REPO._GenericValidatorServices.Validate(contactDto.Id, id, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        var fromDb = await _GENERIC_REPO.Contacts.GetByPredicate(
           x => x.Id == id && x.Deleted == DateTime.MinValue,
           null,
           selector => selector,
           null
        );

        var toUpdate = contactDto.ToUpdate(fromDb);

         _GENERIC_REPO.Contacts.Update(toUpdate);

        return await _GENERIC_REPO.Save();
    }
}
