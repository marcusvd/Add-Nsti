namespace Application.Helpers.Tools.Cnpj;

using System.Threading.Tasks;
using Helpers.Tools.CpfCnpj.Dtos;

public interface ICpfCnpjGetDataServices
{
        Task<BusinessDataDto> CpfCnpjQueryAsync(string cnpj);
}
