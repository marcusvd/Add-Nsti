using Microsoft.EntityFrameworkCore;

using Application.Businesses.Dtos;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.System.Businesses;
using Domain.Entities.System.Companies;
using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;
using Application.BusinessesServices.Dtos.Auth;


namespace Application.BusinessesServices.Services.Profile;

public partial class BusinessProfileServices : IBusinessProfileServices
{
    private readonly IUnitOfWork _genericRepo;
    // private readonly IValidatorsInject _validatorsInject;

    public BusinessProfileServices(
                    IUnitOfWork genericRepo
                    // IValidatorsInject validatorsInject
      )
    {
        _genericRepo = genericRepo;
        // _validatorsInject = validatorsInject;
    }

    public async Task<BusinessProfile> GetBusinessWithCompanyAsync(string businessProfileId)
    {
        return await _genericRepo.BusinessesProfiles.GetByPredicate(
            x => x.BusinessAuthId == businessProfileId,
            add => add.Include(x => x.Companies),
            selector => selector,
            null
            );
    }
     
    public async Task<BusinessProfile> GetBusinessAsync(int id) => await GetBusinessesAsync(id);
  
    private protected async Task<BusinessProfile> GetBusinessesAsync(int id)
    {
        return await _genericRepo.BusinessesProfiles.GetByPredicate(
            x => x.Id == id,
            null,
            selector => selector,
            null
            );
    }
    public async Task<BusinessProfile> GetByBusinessProfileIdAsync(string id) => await GetByProfileIdAsync(id);
  
    private protected async Task<BusinessProfile> GetByProfileIdAsync(string BusinessProfileId)
    {
        return await _genericRepo.BusinessesProfiles.GetByPredicate(
            x => x.BusinessAuthId == BusinessProfileId,
            null,
            selector => selector,
            null
            );
    }

   
}
