
using Domain.Entities.Authentication;
using Application.Shared.Dtos;

using UnitOfWork.Persistence.Operations;
using Application.Exceptions;
using Application.Helpers.Inject;
using Application.Auth.IdentityTokensServices;
using Application.Auth.Register.Extends;
using Application.CompaniesServices.Services.Auth;
using Application.CompaniesServices.Services.Profile;
using Application.BusinessesServices.Services.Profile;
using Application.BusinessesServices.Services.Auth;
using Application.EmailServices.Services;
using Application.Auth.Register.Dtos;
using Application.Auth.Roles.Services;
using Application.Auth.JwtServices;
using Microsoft.AspNetCore.Identity;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;


namespace Application.Auth.Register.Services;

public class RegisterUserAccountServices : RegisterServicesBase, IRegisterUserAccountServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly UserManager<UserAccount> _userManager;
    private readonly IValidatorsInject _validatorsInject;
    private readonly ICompanyAuthServices _companyAuthServices;
    private readonly IBusinessProfileServices _businessProfileServices;
    private readonly IBusinessAuthServices _businessAuthServices;
    private readonly IRolesServices _rolesServices;
    private readonly IJwtServices _jwtServices;
    private readonly IEmailUserAccountServices _emailUserAccountServices;


    public RegisterUserAccountServices(
          IUnitOfWork genericRepo,
          UserManager<UserAccount> userManager,
          IValidatorsInject validatorsInject,
          IIdentityTokensServices identityTokensServices,
          ICompanyAuthServices companyAuthServices,
          IBusinessProfileServices businessProfileServices,
          IBusinessAuthServices businessAuthServices,
          ISmtpServices emailServices,
          IRolesServices rolesServicer,
          IJwtServices jwtServices,
          IEmailUserAccountServices emailUserAccountServices

      ) : base(userManager, identityTokensServices, emailServices)
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
        _userManager = userManager;
        _companyAuthServices = companyAuthServices;
        _businessProfileServices = businessProfileServices;
        _businessAuthServices = businessAuthServices;
        _rolesServices = rolesServicer;
        _jwtServices = jwtServices;
        _emailUserAccountServices = emailUserAccountServices;
    }

    public async Task<UserToken> AddUserExistingCompanyAsync(AddUserExistingCompanyDto request, int companyAuthId)
    {
        _validatorsInject.GenericValidators.IsObjNull(request);

        _validatorsInject.GenericValidators.Validate(request.companyAuthId, companyAuthId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        await ValidateUniqueUserCredentials(request.UserName, request.Email);

        var userAccount = await MakeEntities(request, companyAuthId);

        var registerResult = await _genericRepo.Save();

        var emailResult = await _emailUserAccountServices.SendConfirmEmailAsync(registerResult, userAccount);

        if (emailResult.Success)
        {
            var subscriberRules = await _rolesServices.AddSubscriberRules(userAccount.Email);

            await _rolesServices.UpdateUserRoles(subscriberRules);
        }

        return await _jwtServices.CreateAuthenticationResponseAsync(userAccount);

    }

    private async Task<UserAccount> MakeEntities(AddUserExistingCompanyDto request, int companyAuthId)
    {
        var userProfileId = Guid.NewGuid().ToString();

        var companyAuth = await _companyAuthServices.GetCompanyAuthAsync(companyAuthId);

        var userAccount = AddUserExistingCompanyDto.CreateUserAccount(request, companyAuth.BusinessAuthId, userProfileId);

        var businessAuth = await _businessAuthServices.GetBusinessAsync(companyAuth.BusinessAuthId);

        var businessProfile = await _businessProfileServices.GetByBusinessProfileIdAsync(businessAuth.BusinessProfileId);

        var creationResult = await _userManager.CreateAsync(userAccount, request.Password);

        await AssociationsAndProfileCreation(creationResult.Succeeded, companyAuthId, userAccount.Id, userProfileId, businessProfile.Id, request?.Contact, request?.Address);

        return userAccount;
    }
    private Task<bool> AssociationsAndProfileCreation(bool result, int companyAuthId, int userAccountId, string userProfileId, int businessProfileId, ContactDto contact, AddressDto address)
    {
        if (result)
        {
            var userAccountsCompanies = new CompanyUserAccount() { CompanyAuthId = companyAuthId, UserAccountId = userAccountId };
            _genericRepo.CompaniesUserAccounts.Add(userAccountsCompanies);

            var userProfile = AddUserExistingCompanyDto.CreateUserProfile(userProfileId, contact, address, businessProfileId);
            _genericRepo.UsersProfiles.Add(userProfile);
        }

        return Task.FromResult(result);
    }


}