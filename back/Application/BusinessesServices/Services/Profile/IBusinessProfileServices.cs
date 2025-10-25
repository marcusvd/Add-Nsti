using Domain.Entities.System.Businesses;
using Application.BusinessesServices.Dtos.Auth;


namespace Application.BusinessesServices.Services.Profile;

public interface IBusinessProfileServices
{
        Task<BusinessProfile> GetBusinessWithCompanyAsync(string businessProfileId);
        Task<BusinessProfile> GetBusinessAsync(int id);
        Task<BusinessProfile> GetByBusinessProfileIdAsync(string id);
}