using Application.Shared.Dtos;

namespace Application.Shared.Operations;

public interface IContactServices
{
    Task<bool> AddAsync(ContactDto ContactDto);
    Task<bool> UpdateAsync(ContactDto contactDto, int id);
}