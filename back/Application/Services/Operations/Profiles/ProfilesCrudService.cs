
using UnitOfWork.Persistence.Operations;
using Application.Services.Shared.Mappers.BaseMappers;
using Application.Services.Operations.Profiles.Dtos;
using Domain.Entities.System.Profiles;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Operations.Companies.Dtos;

namespace Application.Services.Operations.Companies;

public class ProfilesCrudService : IProfilesCrudService
{

    private readonly IObjectMapper _mapper;
    private readonly IUnitOfWork _GENERIC_REPO;
    public ProfilesCrudService(
                    IObjectMapper mapper,
                    IUnitOfWork GENERIC_REPO)
    {
        _mapper = mapper;
        _GENERIC_REPO = GENERIC_REPO;
    }


    public async Task<bool> AddUserProfileAsync(UserProfileDto entityDto)
    {
        if (entityDto == null) throw new Exception("Objeto era nulo.");

        var entityConvertedToDb = _mapper.Map<UserProfileDto, UserProfile>(entityDto);

        _GENERIC_REPO.UsersProfiles.Add(entityConvertedToDb);

        return await _GENERIC_REPO.save();

    }
    public async Task<bool> AddBusinessesProfilesAsync(BusinessProfileDto entityDto)
    {
        if (entityDto == null) throw new Exception("Objeto era nulo.");

        var entityConvertedToDb = entityDto.ToEntity();

        var company = _mapper.Map<CompanyProfileDto,CompanyProfile>(entityDto.Companies.ToList());

        entityConvertedToDb.Companies = company.ToList();

        _GENERIC_REPO.BusinessesProfiles.Add(entityConvertedToDb);

        return await _GENERIC_REPO.save();

    }
}