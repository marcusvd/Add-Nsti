using System;
using System.Threading.Tasks;
using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Companies.Dtos;
using Microsoft.EntityFrameworkCore;
using Application.Services.Shared.Dtos.Mappers;
using System.Collections.Generic;

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
        // public async Task<CompanyDto> GetByIdStockIncludedAsync(int id)
        // {
        //     var entityFromDb = await _GENERIC_REPO.Companies.GetByIdStockIncludedAsync(id);

        //     if (entityFromDb == null) throw new Exception("Objeto era nulo.");

        //     return _MAP.Map<CompanyDto>(entityFromDb);
        // }

        public async Task<List<CompanyDto>> GetAllAsync()
        {
            var entityFromDb = await _GENERIC_REPO.Companies.Get().ToListAsync();

            if (entityFromDb == null) throw new Exception("Objeto era nulo.");

            return _mapper.CompanyListMake(entityFromDb);
        }



    }
}