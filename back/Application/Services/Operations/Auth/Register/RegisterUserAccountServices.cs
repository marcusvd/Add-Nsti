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
using Application.Exceptions;
using UnitOfWork.Persistence.Operations;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Shared.Dtos;
using Application.Services.Operations.Auth.Account.dtos;


namespace Application.Services.Operations.Auth.Register;

public class RegisterUserAccountServices : AuthenticationBase, IRegisterUserAccountServices
{
    private readonly ILogger<RegisterUserAccountServices> _logger;
    private readonly UserManager<UserAccount> _userManager;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly IAccountManagerServices _accountManagerServices;
    private readonly IUnitOfWork _GENERIC_REPO;


    // private readonly IUrlHelper _url;
    public RegisterUserAccountServices(
          UserManager<UserAccount> userManager,
          JwtHandler jwtHandler,
          IUrlHelper url,
          IAccountManagerServices accountManagerServices,
          AuthGenericValidatorServices genericValidatorServices,
          IUnitOfWork GENERIC_REPO,
          ILogger<RegisterUserAccountServices> logger
      ) : base(userManager, jwtHandler, logger, url)
    {
        _userManager = userManager;
        _accountManagerServices = accountManagerServices;
        // _url = url;
        _genericValidatorServices = genericValidatorServices;
        _GENERIC_REPO = GENERIC_REPO;
        _logger = logger;
    }

    public async Task<UserToken> AddUserExistingCompanyAsync(AddUserExistingCompanyDto user, int companyId)
    {
        _genericValidatorServices.IsObjNull(user);

        _genericValidatorServices.Validate(user.companyAuthId, companyId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        await ValidateUniqueUserCredentials(user);

        var userProfileId = Guid.NewGuid().ToString();

        var companyAuth = await GetCompanyAuthAsync(user.companyAuthId);

        var userAccount = CreateUserAccount(user, companyAuth.BusinessId, userProfileId).ToEntity() ?? (UserAccount)_genericValidatorServices.ReplaceNullObj<UserAccount>();

        var creationResult = await _userManager.CreateAsync(userAccount, user.Password);

        companyAuth.CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = companyAuth, UserAccount = userAccount });

        var businessAuth = await GetBusinessAuthAsync(companyAuth.BusinessId);

        businessAuth.Companies.Add(companyAuth);

        var businessProfile = await GetBusinessProfileAsync(businessAuth.BusinessProfileId);

        _GENERIC_REPO.BusinessesAuth.Update(businessAuth);

        var userProfile = CreateUserProfile(userProfileId, businessProfile.Id, user.Contact ?? (ContactDto)_genericValidatorServices.ReplaceNullObj<ContactDto>(), user.Address ?? (AddressDto)_genericValidatorServices.ReplaceNullObj<AddressDto>()).ToEntity();


        _GENERIC_REPO.UsersProfiles.Add(userProfile);

        var userProfileResult = await _GENERIC_REPO.Save();

        ResultUserCreation(creationResult.Succeeded, userProfileResult, userAccount.Email, creationResult.Errors.ToString() ?? ($"User creation failed for {userAccount.Email}."));

        var genToken = GenerateUrlTokenEmailConfirmation(userAccount, "ConfirmEmailAddress", "auth");

        var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/confirm-email", "api/auth/ConfirmEmailAddress", "I.M - Link para confirmação de e-mail"]);

        await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.WelcomeMessage());

        var admRole = CreateRole("Users", "Usuários");
        var updateRole = CreateUpdateUserRole(userAccount.Email, "Users", "Usuários", false);

        //TODO: Move this to seeding class.
        await _accountManagerServices.CreateRoleAsync(admRole);

        await _accountManagerServices.UpdateUserRoles(updateRole);

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
            _logger.LogWarning("Duplicate username attempt: {UserName}", register.UserName);
            throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);
        }

        if (await IsEmailDuplicate(register.Email))
        {
            _logger.LogWarning("Duplicate email attempt: {Email}", register.Email);
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
        var userAccount = await _userManager.FindByNameAsync(userName);

        return userAccount != null;
    }
    private async Task<bool> IsEmailDuplicate(string email)
    {
        var userAccount = await _userManager.FindByEmailAsync(email);

        return userAccount != null;
    }
   

  
   
    //     private string FormatEmailUrl(string baseUrl, string urlWithToken, string replace, UserAccount userAccount,DataConfirmEmail dataConfirmEmail)
    //     {


    //         // Assunto: Bem-vindo ao I.M – Confirmação de Cadastro

    //         string mensagemBoasVindas = $@"

    //     {dataConfirmEmail.SubjectEmail}

    // Olá {userAccount.NormalizedUserName},

    // Seja muito bem-vindo ao I.M, o seu novo sistema de gestão de ordens de serviço!

    // Estamos felizes por tê-lo conosco. Este e-mail confirma que o endereço utilizado no cadastro está correto e ativo. Para concluir seu registro e começar a usar o sistema, basta clicar no botão abaixo:

    // Confirme seu e-mail clicando no link abaixo:

    // 🔗 {baseUrl}{urlWithToken.Replace(replace, "")}

    // O I.M foi criado para tornar sua rotina mais eficiente, organizada e segura. A partir de agora, você poderá acompanhar suas ordens de serviço com mais agilidade e controle.

    // Se você não realizou esse cadastro, por favor ignore este e-mail.

    // Ficou com alguma dúvida? Nossa equipe está pronta para ajudar.

    // Atenciosamente,  
    // Equipe I.M  
    // suporte@im.com.br
    // ";

    //         return mensagemBoasVindas;
    //     }

}