using Domain.Entities.Authentication;
using Application.Exceptions;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Shared.Mappers.BaseMappers;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Persistence.Operations;
using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.System.Profiles;
using Application.Services.Shared.Dtos;
using Application.Services.Operations.Profiles.Dtos;

namespace Application.Services.Operations.Auth.CompanyAuthServices;


public class CompanyAuthServices : ICompanyAuthServices
{
    private readonly IUnitOfWork _GENERIC_REPO;
    // private readonly IObjectMapper _objectMapper;

    public CompanyAuthServices(
          IUnitOfWork GENERIC_REPO
        // //   IObjectMapper objectMapper
      )
    {
        _GENERIC_REPO = GENERIC_REPO;
        // _objectMapper = objectMapper;
    }

    public async Task<CompanyAuthDto> GetCompanyAuthAsync(int id)
    {
        var result = await _GENERIC_REPO.CompaniesAuth.GetByPredicate(
            x => x.Id == id && x.Deleted == DateTime.MinValue,
            null,
            selector => selector,
            null
            );

        if (result == null) throw new Exception(GlobalErrorsMessagesException.IsObjNull);

        return result.ToDto();
    }
    public async Task<CompanyAuthDto> GetCompanyAuthFullAsync(int id)
    {
        var companyAuth = await _GENERIC_REPO.CompaniesAuth.GetByPredicate(
            x => x.Id == id && x.Deleted == DateTime.MinValue,
            add => add.Include(x => x.CompanyUserAccounts),
            selector => selector,
            null
            );

        var result = companyAuth ?? (CompanyAuth)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<CompanyAuth>();

        return result.ToDto();
    }
    public async Task<CompanyProfileDto> GetCompanyProfileFullAsync(string companyAuthId)
    {
        var companyProfile = await _GENERIC_REPO.CompaniesProfile.GetByPredicate(
            x => x.CompanyAuthId == companyAuthId && x.Deleted == DateTime.MinValue,
            add => add.Include(x => x.Address)
            .Include(x => x.Contact),
            selector => selector,
            null
            );

        var result = companyProfile ?? (CompanyProfile)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<CompanyProfile>();

        return result.ToDto();
    }
    public async Task<List<UserAccountDto>> GetUsersByCompanyIdAsync(int companyAuthId)
    {
        var companyUserAccount = await GetCompanyUserAccountByCompanyId(companyAuthId);

        var userAccounts = companyUserAccount.Select(x => x.UserAccount).ToList();

        return userAccounts.Select(x => x.ToDto()).ToList() ?? [];
    }
    private async Task<List<CompanyUserAccount>> GetCompanyUserAccountByCompanyId(int companyAuthId)
    {
        var companiesUsers = await _GENERIC_REPO.CompaniesUserAccounts.Get(
            x => x.CompanyAuthId == companyAuthId && x.Deleted == DateTime.MinValue,
              add => add.Include(x => x.UserAccount),
            selector => selector
            ).ToListAsync();

        if (companiesUsers == null) return new List<CompanyUserAccount>();

        return companiesUsers;
    }
    public async Task<UserAuthProfileDto> GetUserByIdFullAsync(int id)
    {

        var userAccount = await GetUserAccountByIdAsync(id);
        var userprofile = await GetUserProfileByProfileIdAsync(userAccount.UserProfileId);

        return MakerUserAccountProfile(userAccount, userprofile);
    }
    private UserAuthProfileDto MakerUserAccountProfile(UserAccount userAccountAuth, UserProfile userProfile)
    {
        return new UserAuthProfileDto()
        {
            UserAccountAuth = userAccountAuth.ToDto(),
            UserAccountProfile = userProfile.ToDto()
        };
    }
    private async Task<UserAccount> GetUserAccountByIdAsync(int id)
    {
        return await _GENERIC_REPO.UsersAccounts.GetByPredicate(
                x => x.Id == id,
                null,
                selector => selector,
                null
                );
    }
    private async Task<UserProfile> GetUserProfileByProfileIdAsync(string userProfileId)
    {
        return await _GENERIC_REPO.UsersProfiles.GetByPredicate(
         x => x.UserAccountId == userProfileId,
         add => add.Include(x => x.Address)
         .Include(x => x.Contact),
         selector => selector,
         null
         );
    }
    public Task UpdateCompanyAuth(CompanyAuthDto companyAuth)
    {
        CompanyAuth update = companyAuth.ToEntity();

        _GENERIC_REPO.CompaniesAuth.Update(update);

        return Task.CompletedTask;
    }

}
