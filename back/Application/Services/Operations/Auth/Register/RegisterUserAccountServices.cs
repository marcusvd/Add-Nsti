using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication.Helpers;
using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Account;
using Authentication.Jwt;
using Application.Services.Operations.Companies;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Profiles.Dtos;
using Application.Services.Operations.Auth.CompanyAuthServices;
using Application.Exceptions;


namespace Application.Services.Operations.Auth.Register;

public class RegisterUserAccountServices : AuthenticationBase, IRegisterUserAccountServices
{
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly UserManager<UserAccount> _userManager;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly IAccountManagerServices _accountManagerServices;
    private readonly ICompanyProfileAddService _companyAddService;
    private readonly ICompanyAuthServices _companyAuthService;
    private readonly IProfilesCrudService _profilesCrudService;

    private readonly JwtHandler _jwtHandler;
    private readonly IUrlHelper _url;
    public RegisterUserAccountServices(
          UserManager<UserAccount> userManager,
          JwtHandler jwtHandler,
          IUrlHelper url,
          IAccountManagerServices accountManagerServices,
          AuthGenericValidatorServices genericValidatorServices,
          ICompanyProfileAddService companyAddService,
          ICompanyAuthServices companyAuthService,
          ILogger<AuthGenericValidatorServices> logger,
          IProfilesCrudService profilesCrudService
      ) : base(userManager, jwtHandler)
    {
        _userManager = userManager;
        _accountManagerServices = accountManagerServices;
        _jwtHandler = jwtHandler;
        _url = url;
        _genericValidatorServices = genericValidatorServices;
        _companyAddService = companyAddService;
        _companyAuthService = companyAuthService;
        _logger = logger;
        _profilesCrudService = profilesCrudService;
    }

    public async Task<UserToken> AddUserExistingCompanyAsync(AddUserExistingCompanyDto user, int companyId)
    {

        _genericValidatorServices.IsObjNull(user);

        _genericValidatorServices.Validate(user.companyAuthId, companyId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        await ValidateUniqueUserCredentials(user);

        var userProfileId = Guid.NewGuid().ToString();

        var companyAuth = await _companyAuthService.GetCompanyAuthAsync(user.companyAuthId);
       
        var userAccount = CreateUserAccount(user, companyAuth.BusinessId, userProfileId);

        var creationResult = await _userManager.CreateAsync(userAccount, user.Password);

        companyAuth.CompanyUserAccounts.Add(new CompanyUserAccount { CompanyAuth = companyAuth, UserAccount = userAccount });

        var userProfile = CreateUserProfile(userProfileId);

        await _companyAuthService.UpdateCompanyAuth(companyAuth);

        var userProfileResult = await _profilesCrudService.AddUserProfileAsync(userProfile);

        if (!userProfileResult)
        {
            _logger.LogError("UserProfile creation failed for {Email}. Errors: {Errors}", "", "");

            throw new AuthServicesException("Error user profile create.");
        }

        if (!creationResult.Succeeded)
        {
            _logger.LogError("User creation failed for {Email}. Errors: {Errors}",
            userAccount.Email, string.Join(", ", creationResult.Errors));

            throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenRegisterUserAccount);
        }

        await SendEmailConfirmationAsync(userAccount);

        var admRole = new UpdateUserRole
        {
            UserName = userAccount.Email,
            Role = "Users",
            DisplayRole = "Usuários",
            Delete = false
        };

        //TODO: Move this to seeding class.
        await _accountManagerServices.CreateRoleAsync(new RoleDto { Name = admRole.Role, DisplayRole = admRole.DisplayRole });

        await _accountManagerServices.UpdateUserRoles(admRole);

        return await CreateAuthenticationResponseAsync(userAccount);
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

        private UserAccount CreateUserAccount(AddUserExistingCompanyDto user, int businessAuthId, string userProfileId)
        {


            var userAccount = new UserAccount()
            {
                DisplayUserName = user.UserName,
                UserName = user.Email,
                Email = user.Email,
                UserProfileId = userProfileId,
                BusinessAuthId = businessAuthId
            };

            // 


            return userAccount;
        }
    
        private UserProfileDto CreateUserProfile(string userAccountId)
        {
            var userProfileDto = new UserProfileDto()
            {
                Id = 0,
                UserAccountId = userAccountId,
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

    private async Task SendEmailConfirmationAsync(UserAccount userAccount)
    {
        try
        {
            var confirmationUrl = await GenerateEmailUrl(userAccount);

            if (string.IsNullOrEmpty(confirmationUrl))
            {
                _logger.LogError("Failed to generate email confirmation URL for {Email}", userAccount.Email);
                throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
            }

            var formattedUrl = FormatEmailUrl("http://localhost:4200/confirm-email", confirmationUrl, "api/auth/ConfirmEmailAddress", userAccount);

            await SendAsync(To: userAccount.Email, Subject: "I.M - Link para confirmação de e-mail", Body: formattedUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending confirmation email to {Email}", userAccount.Email);
            throw;
        }
    }

    private async Task<string> GenerateEmailUrl(UserAccount userAccount)
    {
        var urlConfirmMail = _url.Action("ConfirmEmailAddress", "auth", new
        {
            token = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount),
            email = userAccount.Email

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }

    private string FormatEmailUrl(string baseUrl, string urlWithToken, string replace, UserAccount userAccount)
    {
        string mensagemBoasVindas = $@"
Assunto: Bem-vindo ao I.M – Confirmação de Cadastro

Olá {userAccount.NormalizedUserName},

Seja muito bem-vindo ao I.M, o seu novo sistema de gestão de ordens de serviço!

Estamos felizes por tê-lo conosco. Este e-mail confirma que o endereço utilizado no cadastro está correto e ativo. Para concluir seu registro e começar a usar o sistema, basta clicar no botão abaixo:

Confirme seu e-mail clicando no link abaixo:

🔗 {baseUrl}{urlWithToken.Replace(replace, "")}

O I.M foi criado para tornar sua rotina mais eficiente, organizada e segura. A partir de agora, você poderá acompanhar suas ordens de serviço com mais agilidade e controle.

Se você não realizou esse cadastro, por favor ignore este e-mail.

Ficou com alguma dúvida? Nossa equipe está pronta para ajudar.

Atenciosamente,  
Equipe I.M  
suporte@im.com.br
";

        return mensagemBoasVindas;
    }

}