using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Companies.Dtos;
using Microsoft.EntityFrameworkCore;
using Application.Services.Shared.Mappers.BaseMappers;
using Domain.Entities.System.BusinessesCompanies;

namespace Application.Services.Operations.Companies
{
    public class CompanyGetService : ICompanyGetService
    {

        private readonly ICommonObjectMapper _mapper;
        private readonly IUnitOfWork _GENERIC_REPO;
        public CompanyGetService(
                        ICommonObjectMapper mapper,
                        IUnitOfWork GENERIC_REPO)
        {
            _mapper = mapper;
            _GENERIC_REPO = GENERIC_REPO;
        }
        
        public async Task<List<CompanyProfileDto>> GetAllAsync()
        {
            var entityFromDb = await _GENERIC_REPO.CompaniesProfile.Get().ToListAsync();

            if (entityFromDb == null) throw new Exception("Objeto era nulo.");


            return _mapper.Map<List<CompanyProfile>, List<CompanyProfileDto>>(entityFromDb);
        }



    }
}