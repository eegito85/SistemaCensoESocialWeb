using Microsoft.Extensions.Configuration;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mpce.CensoEsocial.Data.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        public readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmail(string _to, int codigo)
        {
            string _body = ConstroiCorpoEmail(codigo);
            string _subject = "Dados cadastrados com sucesso";
            try
            {
                MailMessage mailMessage = new MailMessage("sistemas.setin@mpce.mp.br", _to, _subject, _body)
                {
                    IsBodyHtml = true
                };

                mailMessage.Priority = MailPriority.Normal;

                SmtpClient client = new SmtpClient("pgjsrv129", 25);
                client.UseDefaultCredentials = true;

                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            await Task.CompletedTask;
        }

        private string ConstroiCorpoEmail(int codigo)
        {
            string sAssunto = "Seus dados foram cadastrados corretamente no Censo E-Social!";
            string strBody = "";
            string url = "http://localhost:62845/Views/Consulta?codigo=";
            url = url + codigo.ToString();

            strBody = strBody + "<html>";
            strBody = strBody + "<head>";
            strBody = strBody + "<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>";
            strBody = strBody + "<title>Untitled Document</title>";
            strBody = strBody + "</head>";
            strBody = strBody + "<body>";

            strBody = strBody + "<table style='font-family: verdana; font-size: 11px; color: #000000;' width='100%'>";
            strBody = strBody + "<tr align=center><td colspan=2><img src='cid:Imagem1' /></td></tr>";
            strBody = strBody + "<tr align=center><td colspan=2></td></tr>";
            strBody = strBody + "<tr style='=font-weight:bold;' align=center><td colspan=2>PROCURADORIA GERAL DE JUSTIÇA</td></tr>";
            strBody = strBody + "<tr style='=font-weight:bold;' align=center><td colspan=2>MINISTÉRIO PÚBLICO DO ESTADO DO CEARÁ</td></tr>";

            strBody = strBody + "<tr><td font-weight:bold'><p><p></td></tr> ";
            strBody = strBody + "</table> ";
            strBody = strBody + "<br><br>";
            strBody = strBody + sAssunto;
            strBody = strBody + "<br><br>";
            strBody = strBody + "<br><br>";
            strBody = strBody + "Dados cadastrados com sucesso!";
            strBody = strBody + "Para conferir seus dados cadatrados, entre no sistema Censo Esocial e clique no link para visualizar seus dados cadastrados.";
            strBody = strBody + "<br><br>";
            strBody = strBody + "Esta é uma  mensagem automática enviada pelo sistema. Não precisa responder.";
            strBody = strBody + "</body>";
            strBody = strBody + "</html>";


            return strBody;
        }

        public async Task EnviarEmailObservacao(string _to, string sDescricao)
        {
            string _body = ConstroiCorpoEmailAlteracao(sDescricao);
            string _subject = "Observações realizadas no ESocial";
            try
            {
                MailMessage mailMessage = new MailMessage("sistemas.setin@mpce.mp.br", _to, _subject, _body)
                {
                    IsBodyHtml = true
                };

                mailMessage.Priority = MailPriority.Normal;

                SmtpClient client = new SmtpClient("pgjsrv129", 25);
                client.UseDefaultCredentials = true;

                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            await Task.CompletedTask;
        }


        private string ConstroiCorpoEmailAlteracao(string sDescricao)
        {
            //string sAssunto = "Seus dados foram cadastrados corretamente no Censo E-Social!";
            string strBody = "";

            strBody = strBody + "<html>";
            strBody = strBody + "<head>";
            strBody = strBody + "<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>";
            strBody = strBody + "<title>Untitled Document</title>";
            strBody = strBody + "</head>";
            strBody = strBody + "<body>";

            strBody = strBody + "<table style='font-family: verdana; font-size: 11px; color: #000000;' width='100%'>";
            strBody = strBody + "<tr align=center><td colspan=2><img src='cid:Imagem1' /></td></tr>";
            strBody = strBody + "<tr align=center><td colspan=2></td></tr>";
            strBody = strBody + "<tr style='=font-weight:bold;' align=center><td colspan=2>PROCURADORIA GERAL DE JUSTIÇA</td></tr>";
            strBody = strBody + "<tr style='=font-weight:bold;' align=center><td colspan=2>MINISTÉRIO PÚBLICO DO ESTADO DO CEARÁ</td></tr>";

            strBody = strBody + "<tr><td font-weight:bold'><p><p></td></tr> ";
            strBody = strBody + "</table> ";
            strBody = strBody + "<br><br>";
            strBody = strBody + "<br><br>";
            strBody = strBody + "Observação realizada:";
            strBody = strBody + "<br><br>";
            strBody = strBody + sDescricao;
            strBody = strBody + "<br><br>";
            strBody = strBody + "<br><br>";
            strBody = strBody + "<br><br>";
            strBody = strBody + "Esta é uma  mensagem automática enviada pelo sistema. Não precisa responder.";
            strBody = strBody + "</body>";
            strBody = strBody + "</html>";


            return strBody;
        }
    }
}
