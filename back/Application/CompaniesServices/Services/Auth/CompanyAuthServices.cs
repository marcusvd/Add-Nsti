using Application.Helpers.Inject;
using Application.CompaniesServices.Dtos.Auth;
using Application.CompaniesServices.Dtos.Mappers;
using Application.CompaniesServices.Extends;
using Domain.Entities.Authentication;
using Domain.Entities.System.Companies;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Persistence.Operations;
using  Application.Shared.Dtos;

namespace Application.CompaniesServices.Services.Auth;

public class CompanyAuthServices : CompanyServicesBase, ICompanyAuthServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;
    public CompanyAuthServices(
                           IUnitOfWork genericRepo,
                           IValidatorsInject validatorsInject
                           ) 
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
    }

    public async Task<CompanyAuth> GetAuthById(int id)
    {
        var fromDb = await _genericRepo.CompaniesAuth.GetByPredicate(
                  x => x.Id == id && x.Deleted == DateTime.MinValue,
                  null,
                  selector => selector,
                  null
                  );

        _validatorsInject.GenericValidators.IsObjNull(fromDb);

        return fromDb;
    }
    public async Task<CompanyAuthDto> GetCompanyAuthAsync(int id)
    {
        var fromDb = await _genericRepo.CompaniesAuth.GetByPredicate(
         x => x.Id == id,
         null,
         selector => selector,
         null
         );

        _validatorsInject.GenericValidators.IsObjNull(fromDb);

        return fromDb;
    }
    public async Task<CompanyAuthDto> GetCompanyAuthFullAsync(int id)
    {
        var fromDb = await _genericRepo.CompaniesAuth.GetByPredicate(
            x => x.Id == id && x.Deleted == DateTime.MinValue,
            add => add.Include(x => x.CompanyUserAccounts),
            selector => selector,
            null
            );

        _validatorsInject.GenericValidators.IsObjNull(fromDb);

        return fromDb;
    }
    public async Task<ApiResponse<CompaniesQtsViewModel>> GetAmountCompaniesByUserIdAsync(int userId)
    {
        var fromDb = await _genericRepo.CompaniesUserAccounts.Get(
            x => x.UserAccountId == userId && x.Deleted == DateTime.MinValue,
            add => add.Include(x => x.CompanyAuth),
            selector => selector,
            null
        ).ToListAsync();

        _validatorsInject.GenericValidators.IsObjNull(fromDb);

        return new ApiResponse<CompaniesQtsViewModel>() { Success = true, Data = GetAmountCompaniesByUserIdHandle(fromDb) };
    }
    private CompaniesQtsViewModel GetAmountCompaniesByUserIdHandle(List<CompanyUserAccount> companiesUsers)
    {
        List<int> companiesIds = new List<int>();

        var companies = companiesUsers.Select(x => (CompanyAuthDto)x.CompanyAuth).ToList();

        if (companies.Count() >= 1)
            companies.ForEach(x => companiesIds.Add(x.Id));

        string singleCompanyName = companies.Count() == 1 ? companies[0].Name : "";

        return new CompaniesQtsViewModel() { AmountCompanies = companies.Count(), Name = singleCompanyName, CompaniesIds = companiesIds };

    }
    public async Task<List<CompanyAuthDto>> GetCompaniesByUserIdAsync(int userId)
    {
        var fromDb = await _genericRepo.CompaniesUserAccounts.Get(
            x => x.UserAccountId == userId && x.Deleted == DateTime.MinValue,
            add => add.Include(x => x.CompanyAuth),
            selector => selector,
            null
        ).ToListAsync();

        var emptyList = _validatorsInject.GenericValidators.EmptyListBuilder(fromDb);

        return emptyList.Select(x => (CompanyAuthDto)x.CompanyAuth).ToList();

    }
    public async Task UpdateCompanyAuthSimple(CompanyAuthDto companyAuth)
    {
        var fromDb = await GetAuthById(companyAuth.Id);

        _validatorsInject.GenericValidators.IsObjNull(fromDb);
        
        var companyAuthToUpdate = fromDb.ToUpdateSimple(companyAuth);

        _genericRepo.CompaniesAuth.Update(companyAuthToUpdate);
    }

}

