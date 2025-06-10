using System.Threading.Tasks;
using Application.Services.Operations.Main.Companies.Dtos;

namespace Application.Services.Operations.Main.Companies
{
    public interface ICompanyAddService
    {
        Task<CompanyDto> AddAsync(CompanyDto entityDto);
    }
}