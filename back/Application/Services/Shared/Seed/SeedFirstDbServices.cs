using System.Threading.Tasks;
using Authentication.Register;
using UnitOfWork.Persistence.Operations;

namespace Application.Services.Shared.Seed.EntitiesSeed;

public class SeedFirstDbServices
{
    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IRegisterServices _iRegisterServices;
    public SeedFirstDbServices(IUnitOfWork GENERIC_REPO, IRegisterServices iRegisterServices)
    {
        _GENERIC_REPO = GENERIC_REPO;
        _iRegisterServices = iRegisterServices;
    }

    public async Task<bool> CheckIfNeededSeed()
    {
        AuthenticationSeed auth = new(_iRegisterServices);
        CustomerSeed_NSTI customers = new();

        await auth.AddUser();

        // _GENERIC_REPO.Customers.AddRangeAsync(customers.CustomerAdd());

        return await _GENERIC_REPO.save();
    }
}