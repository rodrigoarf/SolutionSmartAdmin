using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartAdmin.Domain.Helpers
{
    public sealed class Email
    {
        private string EmailFrom;
        private string EmailTo;
        private string EmailMessage;
        private string EmailSubject;
        private SmtpClient ServerSmtp;

        #region :: Métodos públicos de acesso ::

        /// <summary>
        /// Construtor que envia o e-mail com o Client padrão da aplicação, geralmente é um dos endereços de e-mail ja
        /// configurado no servidor de hospedagem onde a aplicação esta hospedada.
        /// </summary>
        public Email(string emailFrom, string emailTo, string emailSubject, string emailMessage)
        {
            this.EmailFrom = emailFrom;
            this.EmailTo = emailTo;
            this.EmailSubject = emailSubject;
            this.EmailMessage = emailMessage;

            SmtpClient Client = new SmtpClient("smtp.live.com", 587);
            Client.UseDefaultCredentials = true;
            Client.EnableSsl = true;
            Client.Credentials = new System.Net.NetworkCredential(EmailFrom, "rodrigo13");

            this.ServerSmtp = Client;
        }

        /// <summary>
        /// Construtor que aceita Client externo para enviar o e-mail.
        /// Obs: O objeto Client deve estar devidamente configurado conforme o padrão do servidor que vai autenticar o envio.
        /// </summary>
        public Email(string emailFrom, string emailTo, string emailSubject, string emailMessage, SmtpClient Client)
        {
            this.EmailFrom = emailFrom;
            this.EmailTo = emailTo;
            this.EmailSubject = emailSubject;
            this.EmailMessage = emailMessage;
            this.ServerSmtp = Client;
        }

        /// <summary>
        /// Envia o e-mail atraves do Client configurado.
        /// </summary>
        public bool Enviar()
        {
            var retorno = false;  
            var Mensagem = new MailMessage();

            Mensagem.From = new MailAddress(EmailFrom);
            Mensagem.To.Add(EmailTo);
            Mensagem.Subject = EmailSubject;
            Mensagem.BodyEncoding = Encoding.UTF8;
            Mensagem.IsBodyHtml = true;
            Mensagem.Body = CorpoHTML();

            try
            {
                ServerSmtp.Send(Mensagem);
                retorno = true;
            }
            catch (System.Web.HttpException Ex)
            {
                throw new Exception(Ex.Message);
            }

            return (retorno);
        }

        #endregion

        #region :: Métodos privados de acesso ::

        /// <summary>
        /// Monta o corpo HTML para re-envio de senha ao administrador.
        /// </summary>
        private String CorpoHTML()
        {
            var Corpo = new StringBuilder();

            Corpo.AppendLine("<!DOCTYPE html>");
            Corpo.AppendLine("<html>");
            Corpo.AppendLine("<head>");
            Corpo.AppendLine("    <meta name='viewport' content='width=device-width,initial-scale=1'>");
            Corpo.AppendLine("    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1'>");
            Corpo.AppendLine("    <title>E-mail marketing de teste.</title>");
            Corpo.AppendLine("    <style type='text/css'>");
            Corpo.AppendLine("        body");
            Corpo.AppendLine("        {");
            Corpo.AppendLine("            color:#000;");
            Corpo.AppendLine("            font-family: 'Arial', Arial, sans-serif;");
            Corpo.AppendLine("            font-size: 13px;");
            Corpo.AppendLine("            line-height: 1.50em;");
            Corpo.AppendLine("            font-style:normal;");
            Corpo.AppendLine("            font-weight: 400;");
            Corpo.AppendLine("            text-align:justify;");
            Corpo.AppendLine("        }");
            Corpo.AppendLine("        .wrapped");
            Corpo.AppendLine("        {");
            Corpo.AppendLine("            width:790px;");
            Corpo.AppendLine("            margin:0 auto 0 auto; ");
            Corpo.AppendLine("        }");
            Corpo.AppendLine("        h1");
            Corpo.AppendLine("        {");
            Corpo.AppendLine("            font-family: 'Arial',sans-serif;");
            Corpo.AppendLine("            text-align:left;");
            Corpo.AppendLine("            font-size: 36px;");
            Corpo.AppendLine("            letter-spacing:-2px;");
            Corpo.AppendLine("            font-style:normal;");
            Corpo.AppendLine("            font-weight: 700;");
            Corpo.AppendLine("            width:80%;");
            Corpo.AppendLine("            line-height: 1.10em;");
            Corpo.AppendLine("        }");
            Corpo.AppendLine("        hr");
            Corpo.AppendLine("        {");
            Corpo.AppendLine("                border:1px solid #000;");
            Corpo.AppendLine("        }");
            Corpo.AppendLine("    </style>");
            Corpo.AppendLine("</head>");
            Corpo.AppendLine("    <body>");
            Corpo.AppendLine("        <table style='width:100%;'>");
            Corpo.AppendLine("            <tr>");
            Corpo.AppendLine("                <td>");
            Corpo.AppendLine("                    <table class='wrapped'>");
            Corpo.AppendLine("                        <tr>");
            Corpo.AppendLine("                            <td>" + this.EmailMessage + "</td>");
            Corpo.AppendLine("                        </tr>");
            Corpo.AppendLine("                        <tr>");
            Corpo.AppendLine("                            <td>");
            Corpo.AppendLine("                                    <p>");
            Corpo.AppendLine("                                        Se deseja visualizar este informativo direto de seu navegador <a href='#'>clique aqui</a>.<br/>");
            Corpo.AppendLine("                                        Se deseja não receber mais informativos <a href='#'>clique aqui</a>.");
            Corpo.AppendLine("                                    </p> ");
            Corpo.AppendLine("                            </td>");
            Corpo.AppendLine("                        </tr>");
            Corpo.AppendLine("                        <tr>");
            Corpo.AppendLine("                            <td style='background-color:#000; color:#fff; height:40px; text-align:center;'>");
            Corpo.AppendLine("                                    www.techway.me | www.adstudio.me");
            Corpo.AppendLine("                            </td>");
            Corpo.AppendLine("                        </tr>");
            Corpo.AppendLine("                    </table>");
            Corpo.AppendLine("                </td>");
            Corpo.AppendLine("            </tr>");
            Corpo.AppendLine("        </table>");
            Corpo.AppendLine("    </body>");
            Corpo.AppendLine("</html>");

            return (Corpo.ToString());
        }

        #endregion
    }
}
