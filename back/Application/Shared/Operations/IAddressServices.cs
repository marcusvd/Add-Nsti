using Application.Shared.Dtos;

namespace Application.Shared.Operations;

public interface IAddressServices
{
    Task<bool> AddAsync(AddressDto addressDto);
    Task<bool> UpdateAsync(AddressDto addressDto, int id);
}