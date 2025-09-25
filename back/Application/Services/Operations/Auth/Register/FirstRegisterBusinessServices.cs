using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication.Helpers;
using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Account;
using Authentication.Jwt;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Profiles.Dtos;
using Application.Services.Shared.Dtos;
using UnitOfWork.Persistence.Operations;
using Application.Services.Helpers.ServicesLauncher;


namespace Application.Services.Operations.Auth.Register;

public class FirstRegisterBusinessServices : AuthenticationBase, IFirstRegisterBusinessServices
{
    private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;
    private readonly IUnitOfWork _GENERIC_REPO;
    public FirstRegisterBusinessServices(
          //   JwtHandler jwtHandler,
          //   IUrlHelper url,
          IUnitOfWork GENERIC_REPO,
          IAuthServicesInjection AUTH_SERVICES_INJECTION
        //   ILogger<FirstRegisterBusinessServices> logger
      ) : base(GENERIC_REPO, AUTH_SERVICES_INJECTION)
    {
                _GENERIC_REPO = GENERIC_REPO;
                _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
    }

    public async Task<UserToken> RegisterAsync(RegisterModelDto user)
    {

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(user);

        await ValidateUniqueUserCredentials(user);

        var companyId = Guid.NewGuid().ToString();
        var businessProfileId = Guid.NewGuid().ToString();

        var companyAuth = CreateCompanyAuth(user.CompanyName, companyId);
        var companyProfile = CreateCompany(companyId, user);

        var businessAuth = CreateBusinessAuth(companyAuth, businessProfileId);

        var userProfileId = Guid.NewGuid().ToString();
        var userAccount = CreateUserAccount(user, businessAuth, userProfileId);
        var userProfile = CreateUserProfile(userProfileId);

        var businessProfile = CreateBusinessProfile(businessProfileId, companyProfile, userProfile);

        userAccount.CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = companyAuth, UserAccount = userAccount });

        //TODO: Remove this in production - only for testing
        // userAccount.EmailConfirmed = true;

        var creationResult = await _AUTH_SERVICES_INJECTION.UsersManager.CreateAsync(userAccount, user.Password);


        _GENERIC_REPO.BusinessesProfiles.Add(businessProfile.ToEntity());

        var resultProfile = await _GENERIC_REPO.Save();

        if (resultProfile)
        {
            var genToken = GenerateUrlTokenEmailConfirmation(userAccount, "ConfirmEmailAddress", "auth");

            var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/confirm-email", "api/auth/ConfirmEmailAddress", "I.M - Link para confirmação de e-mail"]);

            await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.WelcomeMessage());

            var admRole = CreateRole("SYSADMIN", "Administrador");

            var updateRole = CreateUpdateUserRole(userAccount.Email, "SYSADMIN", "Administrador", false);

            //TODO: Move this to seeding class.
            await CreateRoleAsync(admRole);

            await UpdateUserRoles(updateRole);
        }
        else
            ResultUserCreation(creationResult.Succeeded, resultProfile, userAccount.Email, creationResult.Errors.ToString() ?? ($"User creation failed for {userAccount.Email}."));

        // var claims = await BuildUserClaims(userAccount);

        return await CreateAuthenticationResponseAsync(userAccount);

    }


    private UserAccount CreateUserAccount(RegisterModelDto user, BusinessAuth business, string userProfileId)
    {


        var userAccount = new UserAccount()
        {
            DisplayUserName = user.UserName,
            UserName = user.Email,
            Email = user.Email,
            UserProfileId = userProfileId,
            BusinessAuth = business
        };

        return userAccount;
    }
    private CompanyAuth CreateCompanyAuth(string name, string companyProfileId)
    {

        var companyAuth = new CompanyAuth()
        {
            Id = 0,
            Name = name,
            TradeName = name,
            CompanyProfileId = companyProfileId,
        };
        return companyAuth;
    }
    private CompanyProfileDto CreateCompany(string companyId, RegisterModelDto user)
    {
        if (user.Address is null)
            user.Address = AddressMapper.Incomplete();

        if (user.Contact is null)
            user.Contact = ContactMapper.Incomplete();

        var companyProfile = new CompanyProfileDto()
        {
            Id = 0,
            CompanyAuthId = companyId,
            CNPJ = user.CNPJ,
            Address = user.Address,
            Contact = user.Contact
        };
        return companyProfile;
    }
    private UserProfileDto CreateUserProfile(string userAccountId)
    {
        var userProfileDto = new UserProfileDto()
        {
            Id = 0,
            UserAccountId = userAccountId
        };
        return userProfileDto;
    }
    private BusinessAuth CreateBusinessAuth(CompanyAuth company, string BusinessProfileId)
    {
        var businessAuth = new BusinessAuth()
        {
            Id = 0,
            Name = $"Group BusinessAuth {company.Name}",
            BusinessProfileId = BusinessProfileId,
            // UsersAccounts = new List<UserAccount>() { userAccount }
            Companies = new List<CompanyAuth>() { company },
        };

        return businessAuth;
    }
    private BusinessProfileDto CreateBusinessProfile(string businessProfileId, CompanyProfileDto company, UserProfileDto user)
    {

        var businessProfileDto = new BusinessProfileDto()
        {
            Id = 0,
            BusinessAuthId = businessProfileId,
            UsersAccounts = new List<UserProfileDto>() { user },
            Companies = new List<CompanyProfileDto>() { company }
        };

        return businessProfileDto;
    }


}