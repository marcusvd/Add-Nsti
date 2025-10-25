using Domain.Entities.Authentication;
using UnitOfWork.Persistence.Operations;
using Application.Helpers.Inject;
using Application.Auth.IdentityTokensServices;
using Application.Auth.Register.Extends;
using Application.EmailServices.Services;
using Microsoft.AspNetCore.Identity;
using Application.Auth.Register.Dtos.FirstRegister;
using Application.Auth.Roles.Services;
using Application.Auth.JwtServices;


namespace Application.Auth.Register.Services;

public class FirstRegisterBusinessServices : RegisterServicesBase, IFirstRegisterBusinessServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;
    private readonly UserManager<UserAccount> _userManager;
    private readonly IRolesServices _rolesServicer;
    private readonly IJwtServices _jwtServices;

    public FirstRegisterBusinessServices(

          IUnitOfWork genericRepo,
          IValidatorsInject validatorsInject,
          IIdentityTokensServices identityTokensServices,
          ISmtpServices emailServices,
          UserManager<UserAccount> userManager,
          IRolesServices rolesServicer,
          IJwtServices jwtServices

      ) : base(userManager, identityTokensServices, emailServices)
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
        _userManager = userManager;
        _rolesServicer = rolesServicer;
        _jwtServices = jwtServices;
    }
    public async Task<UserToken> RegisterAsync(RegisterModelDto request)
    {

        _validatorsInject.GenericValidators.IsObjNull(request);

        await ValidateUniqueUserCredentials(request.UserName, request.Email);

        var userAccount = await MakeEntities(request);

        var creationResult = await _userManager.CreateAsync(userAccount, request.Password);

        var resultProfile = await _genericRepo.Save();

        var registerResult = resultProfile && creationResult.Succeeded;

        var emailResult = await SendUrlTokenEmailConfirmation(registerResult, userAccount);

        if (emailResult)
        {
            var subscriberRules = await _rolesServicer.AddSubscriberRules(userAccount.Email);

            await _rolesServicer.UpdateUserRoles(subscriberRules);
        }

        return await _jwtServices.CreateAuthenticationResponseAsync(userAccount);
    }

    private async Task<UserAccount> MakeEntities(RegisterModelDto request)
    {
        var businessProfileId = Guid.NewGuid().ToString();
        var userProfileId = Guid.NewGuid().ToString();

        var companyAuth = RegisterModelDto.CreateCompanyAuth(request.CompanyName, request.CNPJ);
        var companyProfile = RegisterModelDto.CreateCompanyProfile(request);
        var businessAuth = RegisterModelDto.CreateBusinessAuth(companyAuth, businessProfileId);
        var userAccount = RegisterModelDto.CreateUserAccount(request, businessAuth, userProfileId);
        var userProfile = RegisterModelDto.CreateUserProfile(userProfileId, null, null);

        var businessProfile = RegisterModelDto.CreateBusinessProfile(businessProfileId, companyProfile, userProfile);
      
       _genericRepo.BusinessesProfiles.Add(businessProfile);

        userAccount.CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = companyAuth, UserAccount = userAccount });

        return userAccount;
    }

}