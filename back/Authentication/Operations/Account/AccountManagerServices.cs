using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication.Helpers;
using Authentication.Entities;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;


namespace Authentication.Operations.Account;

public class AccountManagerServices : AuthenticationBase, IAccountManagerServices
{
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly UserManager<UserAccount> _userManager;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    private readonly JwtHandler _jwtHandler;
    private readonly IUrlHelper _url;
    public AccountManagerServices(
          UserManager<UserAccount> userManager,
          JwtHandler jwtHandler,

IUrlHelper url,
          AuthGenericValidatorServices genericValidatorServices,
          ILogger<AuthGenericValidatorServices> logger
      ) : base(userManager, jwtHandler)
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
        _genericValidatorServices = genericValidatorServices;
        _logger = logger;
        _url = url;
    }

    public async Task<bool> IsUserExistCheckByEmail(string email) => await IsUserExist(email);


    public async Task<bool> ConfirmEmailAddress(ConfirmEmail confirmEmail)
    {
        var userAccout = await FindUserAsync(confirmEmail.Email);

        var result = await _userManager.ConfirmEmailAsync(userAccout, confirmEmail.Token);

        return result.Succeeded;

    }

    public async Task<bool> ForgotPassword(ForgotPassword forgotPassword)
    {
        var userAccout = await FindUserAsync(forgotPassword.Email);

        await SendEmailConfirmationAsync(userAccout);

        return true;
    }


    private async Task SendEmailConfirmationAsync(UserAccount userAccount)
    {
        try
        {
            var confirmationUrl = await UrlPasswordReset(userAccount);

            if (string.IsNullOrEmpty(confirmationUrl))
            {
                _logger.LogError("Failed to generate email password reset URL for {Email}", userAccount.Email);
                throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
            }

            var formattedUrl = FormatEmailUrl("http://localhost:4200/password-reset", confirmationUrl, "api/auth/ForgotPassword", userAccount);

            await SendAsync(To: userAccount.Email ?? "", Subject: "I.M - Link para recadastramento de senha.", Body: formattedUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending confirmation email to {Email}", userAccount.Email);
            throw;
        }
    }

      public async Task<string> UrlPasswordReset(UserAccount userAccount)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(userAccount);

        var urlReset = _url.Action("ForgotPassword", "auth", new { token, userAccount.Email }) ?? throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink); ;

        return urlReset;
    }

    private string FormatEmailUrl(string baseUrl, string urlWithToken, string replace, UserAccount userAccount)
    {
        string mensagemBoasVindas = $@"
              Olá {userAccount.NormalizedUserName},

    Recebemos uma solicitação para redefinir a senha da sua conta no I.M – Sistema de Gestão de Ordens de Serviço.

    Para continuar com a recuperação de acesso, clique no link abaixo e siga as instruções para criar uma nova senha:

    🔗 {baseUrl}{urlWithToken.Replace(replace, "")}

    Este link é válido por tempo limitado e deve ser utilizado apenas por você. Se você não solicitou essa recuperação, recomendamos que ignore este e-mail. Nenhuma alteração será feita na sua conta sem sua autorização.

    O I.M está comprometido com a segurança e a praticidade no seu dia a dia. Se tiver qualquer dúvida ou dificuldade, nossa equipe de suporte está à disposição para ajudar.

    Atenciosamente,  
    Equipe I.M  
    suporte@im.com.br
";
        return mensagemBoasVindas;
    }

}
   
