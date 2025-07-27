using System.Threading.Tasks;
using Application.Services.Operations.Companies.Dtos;

namespace Application.Services.Operations.Companies
{
    public interface ICompanyAddService
    {
        Task<CompanyDto> AddAsync(CompanyDto entityDto);
    }
}