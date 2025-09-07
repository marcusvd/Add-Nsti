
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
                                
                        Olá {UserAccount.NormalizedUserName},

                        Seja muito bem-vindo ao I.M, o seu novo sistema de gestão de ordens de serviço!

                        Estamos felizes por tê-lo conosco. Este e-mail confirma que o endereço utilizado no cadastro está correto e ativo. Para concluir seu registro e começar a usar o sistema, basta clicar no botão abaixo:

                        Confirme seu e-mail clicando no link abaixo:

                        🔗 {UrlFront}{TokenConfirmationUrl.Replace(UrlBack, "")}

                        O I.M foi criado para tornar sua rotina mais eficiente, organizada e segura. A partir de agora, você poderá acompanhar suas ordens de serviço com mais agilidade e controle.

                        Se você não realizou esse cadastro, por favor ignore este e-mail.

                        Ficou com alguma dúvida? Nossa equipe está pronta para ajudar.

                        Atenciosamente,  
                        Equipe I.M  
                        suporte@im.com.br
                        ";

                return welcomeMessage;
        }
        public string PasswordReset()
        {
                string passwordReset = $@"
                        
                        Olá {UserAccount.NormalizedUserName},

                        Recebemos uma solicitação para redefinir a senha da sua conta no I.M – Sistema de Gestão de Ordens de Serviço.

                        Para continuar com a recuperação de acesso, clique no link abaixo e siga as instruções para criar uma nova senha:

                        🔗 {UrlFront}{TokenConfirmationUrl.Replace(UrlBack, "")}
                        

                        Este link é válido por tempo limitado e deve ser utilizado apenas por você. Se você não solicitou essa recuperação, recomendamos que ignore este e-mail. Nenhuma alteração será feita na sua conta sem sua autorização.

                        O I.M está comprometido com a segurança e a praticidade no seu dia a dia. Se tiver qualquer dúvida ou dificuldade, nossa equipe de suporte está à disposição para ajudar.

                        Atenciosamente,  
                        Equipe I.M  
                        suporte@im.com.br";
                return passwordReset;
        }
        public string EmailUpdated()
        {


                string urlLink = UrlFront + TokenConfirmationUrl.Replace(UrlBack, "");


                string emailUpdated = $@"
                        
                          Olá {UserAccount.NormalizedUserName},

                          Você solicitou a alteração do endereço de e-mail associado à sua conta no I.M – Sistema de Gestão de Ordens de Serviço.

                          Para confirmar essa atualização e garantir que o novo endereço está correto e ativo, clique no link abaixo:

                          🔗 {urlLink}

                          Caso você não tenha solicitado essa alteração, é muito importante que entre em contato imediatamente com nossa equipe de suporte, pois pode se tratar de uma tentativa de acesso não autorizado à sua conta.

                          O I.M preza pela sua segurança e pelo bom funcionamento do seu dia a dia. 

                          Atenciosamente,  
                          Equipe I.M  
                          suporte@im.com.br
                           ";

                return emailUpdated;


        }
}