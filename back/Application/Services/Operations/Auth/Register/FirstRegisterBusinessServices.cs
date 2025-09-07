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


namespace Application.Services.Operations.Auth.Register;

public class FirstRegisterBusinessServices : AuthenticationBase, IFirstRegisterBusinessServices
{
    private readonly ILogger<FirstRegisterBusinessServices> _logger;
    private readonly UserManager<UserAccount> _userManager;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly IAccountManagerServices _accountManagerServices;
    private readonly IUnitOfWork _GENERIC_REPO;
    // private readonly IUrlHelper _url;
    public FirstRegisterBusinessServices(
          UserManager<UserAccount> userManager,
          JwtHandler jwtHandler,
          IUrlHelper url,
          IAccountManagerServices accountManagerServices,
          AuthGenericValidatorServices genericValidatorServices,
          IUnitOfWork GENERIC_REPO,
          ILogger<FirstRegisterBusinessServices> logger
      ) : base(userManager, jwtHandler, logger, url)
    {
        _userManager = userManager;
        _accountManagerServices = accountManagerServices;
        // _url = url;
        _genericValidatorServices = genericValidatorServices;
        _GENERIC_REPO = GENERIC_REPO;
        _logger = logger;
    }

    public async Task<UserToken> RegisterAsync(RegisterModel user)
    {

        _genericValidatorServices.IsObjNull(user);

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

        var creationResult = await _userManager.CreateAsync(userAccount, user.Password);


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
            await _accountManagerServices.CreateRoleAsync(admRole);

            await _accountManagerServices.UpdateUserRoles(updateRole);
        }
        else
            ResultUserCreation(creationResult.Succeeded, resultProfile, userAccount.Email, creationResult.Errors.ToString() ?? ($"User creation failed for {userAccount.Email}."));


        return await CreateAuthenticationResponseAsync(userAccount);

    }

    private async Task ValidateUniqueUserCredentials(RegisterModel register)
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
    private UserAccount CreateUserAccount(RegisterModel user, BusinessAuth business, string userProfileId)
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
    private CompanyProfileDto CreateCompany(string companyId, RegisterModel user)
    {
        if (user.Address is null)
            user.Address = AddressMapper.Incomplete().ToEntity();

        if (user.Contact is null)
            user.Contact = ContactMapper.Incomplete().ToEntity();

        var companyProfile = new CompanyProfileDto()
        {
            Id = 0,
            CompanyAuthId = companyId,
            CNPJ = user.CNPJ,
            Address = user.Address.ToDto(),
            Contact = user.Contact.ToDto()
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
    // private async Task SendEmailConfirmationAsync(UserAccount userAccount)
    // {
    //     try
    //     {
    //         var confirmationUrl = await GenerateUrlTokenEmailConfirmation(userAccount);

    //         if (string.IsNullOrEmpty(confirmationUrl))
    //         {
    //             _logger.LogError("Failed to generate email confirmation URL for {Email}", userAccount.Email);
    //             throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
    //         }

    //         var formattedUrl = FormatEmailUrl("http://localhost:4200/confirm-email", confirmationUrl, "api/auth/ConfirmEmailAddress", userAccount);

    //         await SendAsync(To: userAccount.Email, Subject: "I.M - Link para confirmação de e-mail", Body: formattedUrl);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Error sending confirmation email to {Email}", userAccount.Email);
    //         throw;
    //     }
    // }
    // private async Task<string> GenerateUrlTokenEmailConfirmation(UserAccount userAccount)
    // {
    //     var urlConfirmMail = _url.Action("ConfirmEmailAddress", "auth", new
    //     {
    //         token = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount),
    //         email = userAccount.Email

    //     }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

    //     return urlConfirmMail;
    // }


    //     private string FormatEmailUrl(string baseUrl, string urlWithToken, string replace, UserAccount userAccount)
    //     {
    //         string mensagemBoasVindas = $@"
    // Assunto: Bem-vindo ao I.M – Confirmação de Cadastro

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