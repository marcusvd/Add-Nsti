using Domain.Entities.System.Businesses;
using Application.BusinessesServices.Dtos.Auth;


namespace Application.BusinessesServices.Services.Auth;

public interface IBusinessAuthServices
{
        Task<BusinessAuthDto> GetByBusinessAuthIdAsync(string businessProfileId);
        Task<BusinessAuthDto> GetBusinessFullAsync(int id);
        Task<BusinessAuthDto> GetBusinessAsync(int id);
}