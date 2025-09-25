
using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Profiles.Dtos;
using Application.Exceptions;
using UnitOfWork.Persistence.Operations;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Shared.Dtos;
namespace Application.Services.Operations.Auth.Register;

public class RegisterUserAccountServices : AuthenticationBase, IRegisterUserAccountServices
{

    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;

    public RegisterUserAccountServices(
          IUnitOfWork GENERIC_REPO,
          IAuthServicesInjection AUTH_SERVICES_INJECTION
      ) : base(GENERIC_REPO, AUTH_SERVICES_INJECTION)
    {
        _GENERIC_REPO = GENERIC_REPO;
        _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
        // _logger = logger;
    }

    public async Task<UserToken> AddUserExistingCompanyAsync(AddUserExistingCompanyDto user, int companyId)
    {
        _GENERIC_REPO._GenericValidatorServices.IsObjNull(user);

        _GENERIC_REPO._GenericValidatorServices.Validate(user.companyAuthId, companyId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        await ValidateUniqueUserCredentials(user);

        var userProfileId = Guid.NewGuid().ToString();

        var companyAuth = await GetCompanyAuthAsync(user.companyAuthId);

        var userAccount = CreateUserAccount(user, companyAuth.BusinessId, userProfileId).ToEntity() ?? (UserAccount)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<UserAccount>();

        var creationResult = await _AUTH_SERVICES_INJECTION.UsersManager.CreateAsync(userAccount, user.Password);

        companyAuth.CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = companyAuth, UserAccount = userAccount });

        var businessAuth = await GetBusinessAuthAsync(companyAuth.BusinessId);

        businessAuth.Companies.Add(companyAuth);

        var businessProfile = await GetBusinessProfileAsync(businessAuth.BusinessProfileId);

        _GENERIC_REPO.BusinessesAuth.Update(businessAuth);

        var userProfile = CreateUserProfile(userProfileId, businessProfile.Id, user.Contact ?? (ContactDto)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<ContactDto>(), user.Address ?? (AddressDto)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<AddressDto>()).ToEntity();


        _GENERIC_REPO.UsersProfiles.Add(userProfile);

        var userProfileResult = await _GENERIC_REPO.Save();

        ResultUserCreation(creationResult.Succeeded, userProfileResult, userAccount.Email, creationResult.Errors.ToString() ?? ($"User creation failed for {userAccount.Email}."));

        var genToken = await GenerateUrlTokenEmailConfirmation(userAccount, "ConfirmEmailAddress", "auth");

        var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [genToken, "http://localhost:4200/confirm-email", "api/auth/ConfirmEmailAddress", "I.M - Link para confirmação de e-mail"]);

        await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.WelcomeMessage());

        var admRole = CreateRole("Users", "Usuários");
        var updateRole = CreateUpdateUserRole(userAccount.Email, "Users", "Usuários", false);

        //TODO: Move this to seeding class.
        await CreateRoleAsync(admRole);

        await UpdateUserRoles(updateRole);

        var claims = await BuildUserClaims(userAccount);

        return await CreateAuthenticationResponseAsync(userAccount);
    }


    private async Task<CompanyAuth> GetCompanyAuthAsync(int companyAuthId)
    {
        return await _GENERIC_REPO.CompaniesAuth.GetByPredicate(
         x => x.Id == companyAuthId,
         null,
         selector => selector,
         null
         );
    }
    private async Task<BusinessAuth> GetBusinessAuthAsync(int businessId)
    {
        return await _GENERIC_REPO.BusinessesAuth.GetByPredicate(
                    x => x.Id == businessId,
                    null,
                    selector => selector,
                    null
                    );
    }
    private async Task<BusinessProfile> GetBusinessProfileAsync(string businessProfileId)
    {
        return await _GENERIC_REPO.BusinessesProfiles.GetByPredicate(
            x => x.BusinessAuthId == businessProfileId,

            add => add.Include(x => x.Companies),
            selector => selector,
            null
            );

    }
    private async Task ValidateUniqueUserCredentials(AddUserExistingCompanyDto register)
    {
        if (await IsUserNameDuplicate(register.UserName))
        {
            // _logger.LogWarning("Duplicate username attempt: {UserName}", register.UserName);
            throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);
        }

        if (await IsEmailDuplicate(register.Email))
        {
            // _logger.LogWarning("Duplicate email attempt: {Email}", register.Email);
            throw new AuthServicesException(AuthErrorsMessagesException.EmailAlreadyRegisterd);
        }
    }
    private UserAccountDto CreateUserAccount(AddUserExistingCompanyDto user, int businessAuthId, string userProfileId)
    {

        var userAccount = new UserAccountDto()
        {
            DisplayUserName = user.UserName,
            UserName = user.Email,
            Email = user.Email,
            UserProfileId = userProfileId,
            BusinessAuthId = businessAuthId
        };

        return userAccount;
    }
    private UserProfileDto CreateUserProfile(string userAccountId, int businessProfileId, ContactDto contact, AddressDto address)
    {
        var userProfileDto = new UserProfileDto()
        {
            Id = 0,
            UserAccountId = userAccountId,
            BusinessProfileId = businessProfileId,
            Address = address,
            Contact = contact
        };
        return userProfileDto;
    }
    private async Task<bool> IsUserNameDuplicate(string userName)
    {
        var userAccount = await _AUTH_SERVICES_INJECTION.UsersManager.FindByNameAsync(userName);

        return userAccount != null;
    }
    private async Task<bool> IsEmailDuplicate(string email)
    {
        var userAccount = await _AUTH_SERVICES_INJECTION.UsersManager.FindByEmailAsync(email);

        return userAccount != null;
    }

}