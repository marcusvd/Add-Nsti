using Application.Services.Shared.Dtos;

namespace Application.Services.Shared.Operations;

public interface IAddressServices
{
    Task<bool> AddAsync(AddressDto addressDto);
    Task<bool> UpdateAsync(AddressDto addressDto, int id);
}