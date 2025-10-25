using Application.Auth.Register;
using Application.Auth.Register.Services;
using UnitOfWork.Persistence.Operations;

namespace Application.Shared.Seed.EntitiesSeed;

public class SeedFirstDbServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IFirstRegisterBusinessServices _iFirstRegisterBusinessServices;
    public SeedFirstDbServices(IUnitOfWork genericRepo, IFirstRegisterBusinessServices iFirstRegisterBusinessServices)
    {
        _genericRepo = genericRepo;
        _iFirstRegisterBusinessServices = iFirstRegisterBusinessServices;
    }

    public async Task<bool> CheckIfNeededSeed()
    {
        AuthenticationSeed auth = new(_iFirstRegisterBusinessServices);
        CustomerSeed_NSTI customers = new();

        await auth.AddUser();

        // _genericRepo.Customers.AddRangeAsync(customers.CustomerAdd());

        return await _genericRepo.Save();
    }
}