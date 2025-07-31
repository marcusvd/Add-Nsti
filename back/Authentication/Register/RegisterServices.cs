using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication;
using Authentication.Dtos;


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication.Helpers;
using Authentication.Entities;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;


namespace Authentication.Register;
    public class RegisterServices : AuthenticationBase, IRegisterServices
    {
        private readonly ILogger<GenericValidatorServices> _logger;
        private readonly UserManager<UserAccount> _userManager;
        private readonly GenericValidatorServices _genericValidatorServices;

        // private readonly EmailServer _emailService;
        private readonly JwtHandler _jwtHandler;
        private readonly IUrlHelper _url;
        public RegisterServices(
              UserManager<UserAccount> userManager,
            //   EmailServer emailService,
              JwtHandler jwtHandler,
              IUrlHelper url,

              GenericValidatorServices genericValidatorServices,
              ILogger<GenericValidatorServices> logger
          ) : base(userManager, jwtHandler)
        {
            _userManager = userManager;
            // _emailService = emailService;
            _jwtHandler = jwtHandler;
            _url = url;
            _genericValidatorServices = genericValidatorServices;
            _logger = logger;
        }

        public async Task<UserToken> RegisterAsync(RegisterDto user)
        {
            _genericValidatorServices.IsObjNull(user);

            await ValidateUniqueUserCredentials(user);

            var userAccount = CreateUserAccount(user.Email);
            //TODO: Remove this in production - only for testing
            // userAccount.EmailConfirmed = true;

            var creationResult = await _userManager.CreateAsync(userAccount, user.Password);

            if (!creationResult.Succeeded)
            {
                _logger.LogError("User creation failed for {Email}. Errors: {Errors}",
                userAccount.Email, string.Join(", ", creationResult.Errors));

                throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenRegisterUserAccount);
            }

            await SendEmailConfirmationAsync(userAccount);

            var claimsList = await BuildUserClaims(userAccount);

            return await _jwtHandler.GenerateUserToken(claimsList, userAccount);
        }
        private async Task ValidateUniqueUserCredentials(RegisterDto register)
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
                var confirmationUrl = await GenerateEmailUrl(userAccount, "auth", "ConfirmEmailAddress");

                if (string.IsNullOrEmpty(confirmationUrl))
                {
                    _logger.LogError("Failed to generate email confirmation URL for {Email}", userAccount.Email);
                    throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
                }

                var formattedUrl = FormatEmailUrl("http://sonnyapp.intra/confirm-email", confirmationUrl, "api/auth/ConfirmEmailAddress");

                // await _emailService.SendAsync(To: userAccount.Email, Subject: "Sonny - Link para confirmação de e-mail", Body: formattedUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending confirmation email to {Email}", userAccount.Email);
                throw;
            }
        }
        private UserAccount CreateUserAccount(string email)
        {

            var userAccount = new UserAccount()
            {
                UserName = email,
                Email = email
            };
            return userAccount;
        }

        private async Task<string> GenerateEmailUrl(UserAccount userAccount, string controller, string action)
        {
            return _url.Action(action, controller, new
            {
                token = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount),
                email = userAccount.Email
            });
        }
        private string FormatEmailUrl(string baseUrl, string urlWithToken, string replace)
        {
            return $"{baseUrl}{urlWithToken.Replace(replace, "")}";
        }

    }