using Application.Services.Operations.Auth.Register;
using UnitOfWork.Persistence.Operations;

namespace Application.Services.Shared.Seed.EntitiesSeed;

public class SeedFirstDbServices
{
    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IFirstRegisterBusinessServices _iFirstRegisterBusinessServices;
    public SeedFirstDbServices(IUnitOfWork GENERIC_REPO, IFirstRegisterBusinessServices iFirstRegisterBusinessServices)
    {
        _GENERIC_REPO = GENERIC_REPO;
        _iFirstRegisterBusinessServices = iFirstRegisterBusinessServices;
    }

    public async Task<bool> CheckIfNeededSeed()
    {
        AuthenticationSeed auth = new(_iFirstRegisterBusinessServices);
        CustomerSeed_NSTI customers = new();

        await auth.AddUser();

        // _GENERIC_REPO.Customers.AddRangeAsync(customers.CustomerAdd());

        return await _GENERIC_REPO.save();
    }
}