using System;
using System.Threading.Tasks;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.Companies;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Shared.Dtos.Mappers;

namespace Application.Services.Operations.Companies
{
    public class CompanyAddService : ICompanyAddService
    {

        private readonly ICommonObjectMapper _mapper;
        private readonly IUnitOfWork _GENERIC_REPO;
        public CompanyAddService(
                        ICommonObjectMapper mapper,
                        IUnitOfWork GENERIC_REPO)
        {
            _mapper = mapper;
            _GENERIC_REPO = GENERIC_REPO;
        }


        public async Task<CompanyDto> AddAsync(CompanyDto entityDto)
        {
            if (entityDto == null) throw new Exception("Objeto era nulo.");

            Company entityConvertedToDb = _mapper.CompanyMapper(entityDto);

            _GENERIC_REPO.Companies.Add(entityConvertedToDb);

            if (await _GENERIC_REPO.save())
            {
                var entityFromDb = await _GENERIC_REPO.Companies.GetById(x => x.Id == entityConvertedToDb.Id);
                return _mapper.CompanyMapper(entityFromDb);
            }
            return entityDto;

        }
    }
}