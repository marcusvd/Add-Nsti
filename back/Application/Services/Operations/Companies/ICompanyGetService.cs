using System.Threading.Tasks;
using Application.Services.Operations.Companies.Dtos;

namespace Application.Services.Operations.Companies
{
    public interface ICompanyGetService
    {
        Task<List<CompanyProfileDto>> GetAllAsync();
    }
}