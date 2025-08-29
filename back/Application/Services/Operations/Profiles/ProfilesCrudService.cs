
using UnitOfWork.Persistence.Operations;
using Application.Services.Shared.Mappers.BaseMappers;
using Application.Services.Operations.Profiles.Dtos;
using Domain.Entities.System.Profiles;
using Domain.Entities.System.BusinessesCompanies;

namespace Application.Services.Operations.Companies;
    public class ProfilesCrudService : IProfilesCrudService
    {

        private readonly ICommonObjectMapper _mapper;
        private readonly IUnitOfWork _GENERIC_REPO;
        public ProfilesCrudService(
                        ICommonObjectMapper mapper,
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

            var entityConvertedToDb =_mapper.Map<BusinessProfileDto,BusinessProfile>(entityDto);

            _GENERIC_REPO.BusinessesProfiles.Add(entityConvertedToDb);

            return await _GENERIC_REPO.save();

        }
    }