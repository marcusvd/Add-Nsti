using Microsoft.EntityFrameworkCore;

using Application.Helpers.Inject;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.System.Businesses;

using Application.BusinessesServices.Dtos.Auth;
using Application.BusinessesServices.Extends;

namespace Application.BusinessesServices.Services.Auth;

public partial class BusinessAuthServices : BusinessServicesBase, IBusinessAuthServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;

    public BusinessAuthServices(
                    IUnitOfWork genericRepo,
                    IValidatorsInject validatorsInject
      )
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
    }

    public async Task<BusinessAuthDto> GetBusinessFullAsync(int id) => await GetFullAsync(id);
    private async Task<BusinessAuthDto> GetFullAsync(int id)
    {
        var fromDb = await _genericRepo.BusinessesAuth.GetByPredicate(
                x => x.Id == id && x.Deleted == DateTime.MinValue,
                add =>
                add.Include(x => x.UsersAccounts)
               .Include(x => x.Companies)
               .ThenInclude(x => x.CompanyUserAccounts),
                selector => selector,
                ordeBy => ordeBy.OrderBy(x => x.Name)
                );

        return fromDb;
    }

    public async Task<BusinessAuthDto> GetBusinessAsync(int id)
    {

        var fromDb = await GetBusinessAuthAsync(id);

        _validatorsInject.GenericValidators.IsObjNull(fromDb);

        return fromDb;
    }

    private async Task<BusinessAuth> GetBusinessAuthAsync(int id)
    {
        return await _genericRepo.BusinessesAuth.GetByPredicate(
                    x => x.Id == id && x.Deleted == DateTime.MinValue,
                    null,
                    selector => selector,
                    null);
    }
    public async Task<BusinessAuthDto> GetByBusinessAuthIdAsync(string businessProfileId)
    {

        var fromDb = await GetBusinessAuthIdAsync(businessProfileId);

        _validatorsInject.GenericValidators.IsObjNull(fromDb);

        return fromDb;
    }

    private async Task<BusinessAuth> GetBusinessAuthIdAsync(string businessAuthId)
    {
        return await _genericRepo.BusinessesAuth.GetByPredicate(
                    x => x.BusinessProfileId == businessAuthId && x.Deleted == DateTime.MinValue,
                    null,
                    selector => selector,
                    null);
    }

}
