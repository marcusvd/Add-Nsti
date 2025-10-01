
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Account.dtos;


public class DataConfirmEmail
{
  public required UserAccount UserAccount { get; set; }
  public required string TokenConfirmationUrl { get; set; }
  public required string UrlFront { get; set; }
  public required string UrlBack { get; set; }
  public required string SubjectEmail { get; set; } = "Olá I.M - Link para confirmação de e-mail";

  public string WelcomeMessage()
  {

    string welcomeMessage = $@"
<!DOCTYPE html>
<html lang='pt-BR'>
<head>
  <meta charset='UTF-8'>
  <title>Bem-vindo ao I.M</title>
  <style>
    body {{
      font-family: Arial, sans-serif;
      color: #333;
      line-height: 1.6;
      padding: 20px;
    }}
    .button {{
      display: inline-block;
      padding: 12px 20px;
      margin: 20px 0;
      background-color: #007bff;
      color: white;
      text-decoration: none;
      border-radius: 5px;
    }}
    .footer {{
      margin-top: 40px;
      font-size: 0.9em;
      color: #666;
    }}
  </style>
</head>
<body>
  <p>Olá <strong>{UserAccount.NormalizedUserName}</strong>,</p>

  <p>Seja muito bem-vindo ao <strong>I.M</strong>, o seu novo sistema de gestão de ordens de serviço!</p>

  <p>Estamos felizes por tê-lo conosco. Este e-mail confirma que o endereço utilizado no cadastro está correto e ativo. Para concluir seu registro e começar a usar o sistema, basta clicar no botão abaixo:</p>

  <p><a href='{UrlFront}{TokenConfirmationUrl.Replace(UrlBack, "")}' class='button'>Confirmar e-mail</a></p>

  <p>O I.M foi criado para tornar sua rotina mais eficiente, organizada e segura. A partir de agora, você poderá acompanhar suas ordens de serviço com mais agilidade e controle.</p>

  <p>Se você não realizou esse cadastro, por favor ignore este e-mail.</p>

  <p>Ficou com alguma dúvida? Nossa equipe está pronta para ajudar.</p>

  <div class='footer'>
    <p>Atenciosamente,<br>
    Equipe I.M<br>
    <a href='mailto:suporte@im.com.br'>suporte@im.com.br</a></p>
  </div>
</body>
</html>";

    return welcomeMessage;
  }
  public string PasswordReset()
  {

    string passwordReset = $@"
<!DOCTYPE html>
<html lang=""pt-BR"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Redefinição de Senha - I.M Sistema</title>
    <style>
        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
        }}
        .container {{
            background-color: #ffffff;
            border-radius: 8px;
            padding: 30px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            border: 1px solid #e1e1e1;
        }}
        .header {{
            text-align: center;
            margin-bottom: 25px;
            border-bottom: 1px solid #eeeeee;
            padding-bottom: 20px;
        }}
        .logo {{
            color: #0556cb;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 10px;
        }}
        .title {{
            font-size: 20px;
            font-weight: 600;
            margin: 15px 0;
            color: #1a1a1a;
        }}
        .content {{
            margin: 20px 0;
        }}
        .link-container {{
            background-color: #f0f7ff;
            border: 1px dashed #0556cb;
            border-radius: 6px;
            padding: 20px;
            text-align: center;
            margin: 25px 0;
        }}
        .reset-link {{
            display: inline-block;
            background-color: #0556cb;
            color: white;
            padding: 12px 24px;
            text-decoration: none;
            border-radius: 4px;
            font-weight: bold;
            margin: 10px 0;
        }}
        .reset-link:hover {{
            background-color: #0444a8;
        }}
        .warning {{
            color: #d32f2f;
            font-weight: 600;
            margin: 15px 0;
            background-color: #ffebee;
            padding: 10px;
            border-radius: 4px;
            border-left: 4px solid #d32f2f;
        }}
        .footer {{
            margin-top: 30px;
            padding-top: 20px;
            border-top: 1px solid #eeeeee;
            font-size: 14px;
            color: #666;
        }}
        .greeting {{
            font-weight: bold;
            margin-bottom: 15px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <div class=""logo"">I.M Sistema</div>
            <h1 class=""title"">Redefinição de Senha</h1>
        </div>
        
        <div class=""content"">
            <p class=""greeting"">Olá {UserAccount.NormalizedUserName},</p>
            
            <p>Recebemos uma solicitação para redefinir a senha da sua conta no <strong>I.M – Sistema de Gestão de Ordens de Serviço</strong>.</p>
            
            <p>Para continuar com a recuperação de acesso, clique no botão abaixo e siga as instruções para criar uma nova senha:</p>
            
            <div class=""link-container"">
                <a href=""{UrlFront}{TokenConfirmationUrl.Replace(UrlBack, "")}"" class=""reset-link"">
                    🔗 REDEFINIR MINHA SENHA
                </a>
                <p style=""margin-top: 10px; font-size: 14px; color: #666;"">
                    Ou copie e cole este link no seu navegador:<br>
                    <span style=""word-break: break-all;"">{UrlFront}{TokenConfirmationUrl.Replace(UrlBack, "")}</span>
                </p>
            </div>
            
            <div class=""warning"">
                ⚠️ Este link é válido por tempo limitado 10 minutos e deve ser utilizado apenas por você.
            </div>
            
            <p>Se você não solicitou essa recuperação, recomendamos que ignore este e-mail. Nenhuma alteração será feita na sua conta sem sua autorização.</p>
            
            <p>O <strong>I.M</strong> está comprometido com a segurança e a praticidade no seu dia a dia. Se tiver qualquer dúvida ou dificuldade, nossa equipe de suporte está à disposição para ajudar.</p>
        </div>
        
        <div class=""footer"">
            <p><strong>Atenciosamente,</strong><br>
            Equipe I.M<br>
            <a href=""mailto:suporte@im.com.br"">suporte@im.com.br</a></p>
        </div>
    </div>
</body>
</html>";

    return passwordReset;
    // string passwordReset = $@"

    //         Olá {UserAccount.NormalizedUserName},

    //         Recebemos uma solicitação para redefinir a senha da sua conta no I.M – Sistema de Gestão de Ordens de Serviço.

    //         Para continuar com a recuperação de acesso, clique no link abaixo e siga as instruções para criar uma nova senha:

    //         🔗 {UrlFront}{TokenConfirmationUrl.Replace(UrlBack, "")}


    //         Este link é válido por tempo limitado e deve ser utilizado apenas por você. Se você não solicitou essa recuperação, recomendamos que ignore este e-mail. Nenhuma alteração será feita na sua conta sem sua autorização.

    //         O I.M está comprometido com a segurança e a praticidade no seu dia a dia. Se tiver qualquer dúvida ou dificuldade, nossa equipe de suporte está à disposição para ajudar.

    //         Atenciosamente,  
    //         Equipe I.M  
    //         suporte@im.com.br";
    // return passwordReset;
  }
  public string EmailUpdated()
  {


    string urlLink = UrlFront + TokenConfirmationUrl.Replace(UrlBack, "");


    string emailUpdated = $@"
<!DOCTYPE html>
<html lang=""pt-BR"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Alteração de E-mail - I.M Sistema</title>
    <style>
        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
        }}
        .container {{
            background-color: #ffffff;
            border-radius: 8px;
            padding: 30px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            border: 1px solid #e1e1e1;
        }}
        .header {{
            text-align: center;
            margin-bottom: 25px;
            border-bottom: 1px solid #eeeeee;
            padding-bottom: 20px;
        }}
        .logo {{
            color: #0556cb;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 10px;
        }}
        .title {{
            font-size: 20px;
            font-weight: 600;
            margin: 15px 0;
            color: #1a1a1a;
        }}
        .content {{
            margin: 20px 0;
        }}
        .link-container {{
            background-color: #f0f7ff;
            border: 1px dashed #0556cb;
            border-radius: 6px;
            padding: 20px;
            text-align: center;
            margin: 25px 0;
        }}
        .confirm-link {{
            display: inline-block;
            background-color: #0556cb;
            color: white;
            padding: 12px 24px;
            text-decoration: none;
            border-radius: 4px;
            font-weight: bold;
            margin: 10px 0;
        }}
        .confirm-link:hover {{
            background-color: #0444a8;
        }}
        .warning {{
            color: #d32f2f;
            font-weight: 600;
            margin: 15px 0;
            background-color: #ffebee;
            padding: 15px;
            border-radius: 4px;
            border-left: 4px solid #d32f2f;
        }}
        .security-alert {{
            background-color: #fff3e0;
            border-left: 4px solid #ff9800;
            padding: 15px;
            margin: 20px 0;
            border-radius: 0 4px 4px 0;
        }}
        .footer {{
            margin-top: 30px;
            padding-top: 20px;
            border-top: 1px solid #eeeeee;
            font-size: 14px;
            color: #666;
        }}
        .greeting {{
            font-weight: bold;
            margin-bottom: 15px;
            font-size: 16px;
        }}
        .contact-info {{
            background-color: #e8f5e9;
            padding: 15px;
            border-radius: 4px;
            margin: 20px 0;
            border-left: 4px solid #4caf50;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <div class=""logo"">I.M Sistema</div>
            <h1 class=""title"">Alteração de E-mail</h1>
        </div>
        
        <div class=""content"">
            <p class=""greeting"">Olá {UserAccount.NormalizedUserName},</p>
            
            <p>Você solicitou a alteração do endereço de e-mail associado à sua conta no <strong>I.M – Sistema de Gestão de Ordens de Serviço</strong>.</p>
            
            <p>Para confirmar essa atualização e garantir que o novo endereço está correto e ativo, clique no botão abaixo:</p>
            
            <div class=""link-container"">
                <a href=""{urlLink}"" class=""confirm-link"">
                    📧 CONFIRMAR ALTERAÇÃO DE E-MAIL
                </a>
                <p style=""margin-top: 10px; font-size: 14px; color: #666;"">
                    Ou copie e cole este link no seu navegador:<br>
                    <span style=""word-break: break-all;"">{urlLink}</span>
                </p>
            </div>
            
            <div class=""security-alert"">
                <strong>⚠️ Atenção:</strong> Caso você não tenha solicitado essa alteração, é muito importante que entre em contato imediatamente com nossa equipe de suporte, pois pode se tratar de uma tentativa de acesso não autorizado à sua conta.
            </div>
            
            <div class=""contact-info"">
                <strong>📞 Contato Imediato:</strong><br>
                E-mail: <a href=""mailto:suporte@im.com.br"">suporte@im.com.br</a><br>
                Responder este e-mail também funciona!
            </div>
            
            <p>O <strong>I.M</strong> preza pela sua segurança e pelo bom funcionamento do seu dia a dia.</p>
        </div>
        
        <div class=""footer"">
            <p><strong>Atenciosamente,</strong><br>
            <strong>Equipe I.M</strong><br>
            <a href=""mailto:suporte@im.com.br"">suporte@im.com.br</a></p>
        </div>
    </div>
</body>
</html>";

    return emailUpdated;

  }
}