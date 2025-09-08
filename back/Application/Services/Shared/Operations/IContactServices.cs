using Application.Services.Shared.Dtos;

namespace Application.Services.Shared.Operations;

public interface IContactServices
{
    Task<bool> AddAsync(ContactDto ContactDto);
    Task<bool> UpdateAsync(ContactDto contactDto, int id);
}