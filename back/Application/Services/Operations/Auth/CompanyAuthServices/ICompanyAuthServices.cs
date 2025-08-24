
using Domain.Entities.Authentication;


namespace Application.Services.Operations.Auth.CompanyAuthServices;

public interface ICompanyAuthServices
{
    Task<bool> AddAsync(CompanyAuth entity);
}
