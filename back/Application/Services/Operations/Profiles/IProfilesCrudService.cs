using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Profiles.Dtos;

namespace Application.Services.Operations.Companies;

public interface IProfilesCrudService
{
    Task<bool> AddUserProfileAsync(UserProfileDto entityDto);
    Task<bool> AddBusinessesProfilesAsync(BusinessProfileDto entityDto);
}