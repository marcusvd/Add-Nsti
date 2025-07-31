namespace Authentication.Exceptions;

    public static class AuthErrorsMessagesException
    {
        //ACCOUNTS
        public static readonly string EmailIsNotConfirmed = "1.0|Email precisa ser confirmado! Acesse seus emails, caso não o encontre na caixa de entrada olhe na caixa de spam. Obrigado!";
        public static readonly string EmailAlreadyRegisterd = "1.1|Email ja cadastrado.";
        public static readonly string UserNameAlreadyRegisterd = "1.2|Nome de usuário ja cadastrado.";
        public static readonly string IsEmailConfirmed = "1.3|Email já foi confirmado.";
        public static readonly string InvalidUserNameOrPassword = "1.4|Usuário ou senha incorreto.";
        public static readonly string ErrorIdUpdateUserAccount = "1.5|Dados inválidos. id's não coincidem.";
        public static readonly string UserAccountNotFound = "1.6|Usuário ou senha incorreto."; //this is the real one -> "Usuário não encontrado."
        public static readonly string ErrorWhenRegisterUserAccount = "1.7|Não foi possível cadastrar o usuário, verifique os dados e tente novamente, obrigado!";
        public static readonly string ErrorWhenTryUpdateUserAccount = "1.8|Erro ao tentar atualizar.";
        public static readonly string ErrorWhenGenerateEmailLink = "1.9|O link não pode ser gerado.";
        public static readonly string UnknownError = "1.10|O link não pode ser gerado.";
        public static readonly string UserIsLocked = "1.11|Usuário está bloqueado.";
        public static readonly string ResetPassword = "1.12|Erro durante redefinição de senha.";
        public static readonly string TwoFactorTokenSendFailed = "1.13|Erro durante envio do 2FA token para o {Email}";

        //GENERAL MESSAGES
        public static readonly string ObjectIsNull = "2.0|Objeto era nulo.";
        public static readonly string TokenGenerationProvider = "2.1|Provedor era nulo.";
        public static readonly string ExpiredTokenOrInvalid = "2.2|Token expirado ou inválido.";
    }












