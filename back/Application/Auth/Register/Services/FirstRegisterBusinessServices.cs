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
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Application.Helpers.Tools.Cnpj;
using Application.Helpers.Tools.ZipCode;
using Application.Helpers.Tools.CpfCnpj.Dtos;
using Application.Shared.Dtos;
using Application.CompaniesServices.Dtos.Profile;
using Application.Auth.UsersAccountsServices.Profile;
using Domain.Entities.System.Profiles;
using Domain.Entities.System.Businesses;
using Application.EmailServices.Exceptions;



namespace Application.Auth.Register.Services;

public class FirstRegisterBusinessServices : RegisterServicesBase, IFirstRegisterBusinessServices
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;
    private readonly UserManager<UserAccount> _userManager;
    private readonly IRolesServices _rolesServicer;
    private readonly IJwtServices _jwtServices;
    private readonly IEmailUserAccountServices _emailUserAccountServices;
    // private readonly ICpfCnpjGetDataServices _cpfCnpjGetDataServices;

    public FirstRegisterBusinessServices(

          IUnitOfWork genericRepo,
          IValidatorsInject validatorsInject,
          IIdentityTokensServices identityTokensServices,
          ISmtpServices emailServices,
          UserManager<UserAccount> userManager,
          IRolesServices rolesServicer,
          IJwtServices jwtServices,
          IEmailUserAccountServices emailUserAccountServices
      //   ICpfCnpjGetDataServices cpfCnpjGetDataServices

      ) : base(userManager, identityTokensServices, emailServices)
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
        _userManager = userManager;
        _rolesServicer = rolesServicer;
        _jwtServices = jwtServices;
        _emailUserAccountServices = emailUserAccountServices;
        // _cpfCnpjGetDataServices = cpfCnpjGetDataServices;
    }
    public async Task<UserToken> RegisterAsync(
                                                RegisterModelDto request, ICpfCnpjGetDataServices cpfCnpjGetDataServices,
                                                IZipCodeGetDataServices zipCodeGetDataServices, IPhoneNumberValidateServices phoneNumberValidateServices
                                                )
    {

        _validatorsInject.GenericValidators.IsObjNull(request);

        await ValidateUniqueUserCredentials(request.UserName, request.Email);

        var dataBusiness = await cpfCnpjGetDataServices.CpfCnpjQueryAsync(request.CNPJ);

        var userAccount = await MakeEntities(request, dataBusiness, zipCodeGetDataServices, phoneNumberValidateServices);

        var creationResult = await _userManager.CreateAsync(userAccount, request.Password);

        var resultProfile = await _genericRepo.Save();

        var registerResult = resultProfile && creationResult.Succeeded;

        try
        {
            var emailResult = await _emailUserAccountServices.SendConfirmEmailAsync(registerResult, userAccount);

            if (emailResult.Success)
            {
                var subscriberRules = await _rolesServicer.AddSubscriberRules(userAccount.Email);

                await _rolesServicer.UpdateUserRoles(subscriberRules);
            }

        }
        catch (EmailServicesException ex)
        {
            // if (ex.Message != null)
            // await DestroyUserCreatedWithEmailError(userAccount);
        }

        return await _jwtServices.CreateAuthenticationResponseAsync(userAccount);
    }

    private async Task<UserAccount> MakeEntities(RegisterModelDto request, BusinessDataDto businessData, IZipCodeGetDataServices zipCodeGetDataServices, IPhoneNumberValidateServices phoneNumberValidateServices)
    {
        var businessProfileId = Guid.NewGuid().ToString();
        var userProfileId = Guid.NewGuid().ToString();

        var companyAuth = RegisterModelDto.CreateCompanyAuth(request.CompanyName, request.CNPJ);

        var companyProfile = RegisterModelDto.CreateCompanyProfile(request);

        companyProfile.Address = await zipCodeGetDataServices.ZipCodeQueryAsync(businessData.Cep);

        companyProfile = CompanyWithContact(businessData, companyProfile, phoneNumberValidateServices);

        var businessAuth = RegisterModelDto.CreateBusinessAuth(companyAuth, businessProfileId);
        var userAccount = RegisterModelDto.CreateUserAccount(request, businessAuth, userProfileId);
        var userProfile = RegisterModelDto.CreateUserProfile(userProfileId, null, null);

        var businessProfile = RegisterModelDto.CreateBusinessProfile(businessProfileId, companyProfile, userProfile);

        _genericRepo.BusinessesProfiles.Add(businessProfile);

        userAccount.CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = companyAuth, UserAccount = userAccount });

        return userAccount;
    }

    private CompanyProfileDto CompanyWithContact(BusinessDataDto businessData, CompanyProfileDto companyProfile, IPhoneNumberValidateServices phoneNumberValidateServices)
    {
        var isMobile = phoneNumberValidateServices.IsMobile(businessData.Telefone);

        return AssignValues(companyProfile, businessData, isMobile);
    }
    private CompanyProfileDto AssignValues(CompanyProfileDto companyProfile, BusinessDataDto businessData, string isMobile)
    {
        if (isMobile == PhoneTypeDto.Mobile)
        {
            companyProfile.Contact = new ContactDto() { Email = businessData.Email, Cel = isMobile };
            return companyProfile;
        }

        if (isMobile == PhoneTypeDto.Landline)
        {
            companyProfile.Contact = new ContactDto() { Email = businessData.Email, Landline = isMobile };
            return companyProfile;
        }

        if (isMobile == PhoneTypeDto.Invalid)
            return companyProfile;

        return companyProfile;

    }

    // private async Task<bool> DestroyUserCreatedWithEmailError(UserAccount userAccount)
    // {
    //     _validatorsInject.GenericValidators.IsObjNull(userAccount);

    //     // return (await _userManager.DeleteAsync(userAccount)).Succeeded;

    //     // UserProfile userProfile = await _genericRepo.UsersProfiles.GetByPredicate(x => x.UserAccountId == userAccount.UserProfileId);
    //     // _validatorsInject.GenericValidators.IsObjNull(userProfile);
    //     // _genericRepo.UsersProfiles.Delete(userProfile);

    //     // BusinessAuth businessAuth = await _genericRepo.BusinessesAuth.GetByPredicate(
    //     //     x => x.Id == userAccount.BusinessAuthId,
    //     //     null,
    //     //     selector => selector,
    //     //     null
    //     //     );

    //     // _validatorsInject.GenericValidators.IsObjNull(businessAuth);
    //     _genericRepo.BusinessesAuth.Delete(userAccount.BusinessAuth);

    //     // BusinessProfile businessProfile = await _genericRepo.BusinessesProfiles.GetByPredicate(x => x.BusinessAuthId == businessAuth.BusinessProfileId);
    //     // _validatorsInject.GenericValidators.IsObjNull(businessProfile);

    //     // TimedAccessControl timedAccessControl = await _genericRepo.TimedAccessControls.GetByPredicate(x => x.Id == userAccount.TimedAccessControl!.Id);
    //     // _validatorsInject.GenericValidators.IsObjNull(timedAccessControl);

    //     // _genericRepo.BusinessesProfiles.Delete(businessProfile);
    //     // _genericRepo.TimedAccessControls.Delete(timedAccessControl);
    //     return await _genericRepo.Save();
    // }

    // private Task<bool> UsersRolesClean(int userId)
    // {
    //     _genericRepo.user
    // }

    public async Task<ApiResponse<UserToken>> FirstEmailConfirmation(string email)
    {
        await ValidateUniqueUserCredentials(email, email);
        
        var userToken = await _jwtServices.FirstRegisterEmailValidation(email);

        return await _emailUserAccountServices.FirstEmailConfirmationAsync(userToken);
    }








}