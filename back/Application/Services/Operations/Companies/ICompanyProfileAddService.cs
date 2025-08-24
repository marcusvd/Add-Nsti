using System.Threading.Tasks;
using Application.Services.Operations.Companies.Dtos;

namespace Application.Services.Operations.Companies
{
    public interface ICompanyProfileAddService
    {
        Task<bool> AddAsync(CompanyProfileDto entityDto);
    }
}