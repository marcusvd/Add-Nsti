using System;
using System.Threading.Tasks;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Shared.Mappers.BaseMappers;

namespace Application.Services.Operations.Companies
{
    public class CompanyProfileAddService : ICompanyProfileAddService
    {

        private readonly ICommonObjectMapper _mapper;
        private readonly IUnitOfWork _GENERIC_REPO;
        public CompanyProfileAddService(
                        ICommonObjectMapper mapper,
                        IUnitOfWork GENERIC_REPO)
        {
            _mapper = mapper;
            _GENERIC_REPO = GENERIC_REPO;
        }


        public async Task<bool> AddAsync(CompanyProfileDto entityDto)
        {
            if (entityDto == null) throw new Exception("Objeto era nulo.");

            CompanyProfile entityConvertedToDb = _mapper.Map<CompanyProfileDto, CompanyProfile>(entityDto);

            _GENERIC_REPO.CompaniesProfile.Add(entityConvertedToDb);

            return await _GENERIC_REPO.save();

        }
    }
}