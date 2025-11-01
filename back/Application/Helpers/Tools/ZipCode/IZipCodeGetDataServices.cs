namespace Application.Helpers.Tools.ZipCode;

using System.Threading.Tasks;
using Shared.Dtos;
public interface IZipCodeGetDataServices
{
        Task<AddressDto> ZipCodeQueryAsync(string zipCode);
}
