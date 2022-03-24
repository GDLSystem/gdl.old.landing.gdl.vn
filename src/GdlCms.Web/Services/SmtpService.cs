using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using GdlCms.Web.Controllers;
using GdlCms.Web.ViewModels;
using Umbraco.Core.Logging;

namespace GdlCms.Web.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly ILogger _logger;
        
        public SmtpService(ILogger logger)
        {
            _logger = logger;
        }
        
        public bool SendEmail(string inputEmail,string phone, string name,string bodyEmail)
        {
            try
            {
                var fromEmail = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                var pass = ConfigurationManager.AppSettings["EmailPassword"];
                var toEmail = new MailAddress(ConfigurationManager.AppSettings["EmailReceive"]);
#if DEBUG
                toEmail = new MailAddress("romeohuy@gmail.com"); 
#endif
                string title = string.Format(ConfigurationManager.AppSettings["EmailTitle"], inputEmail, name);

                var useDefaultCredentials = bool.Parse(ConfigurationManager.AppSettings["UseDefaultCredentialsMail"]);
                var enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSslMail"]);
                var smtp = new SmtpClient()
                {
                    Host = ConfigurationManager.AppSettings["HostMail"],
                    Port = int.Parse(ConfigurationManager.AppSettings["PortMail"]),
                    EnableSsl = enableSsl,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromEmail.Address, pass)
                };
                
                var mess = new MailMessage(fromEmail, toEmail)
                {
                    Subject = title,
                    Body = bodyEmail,
                    IsBodyHtml = true
                };

                var ccMailsString = ConfigurationManager.AppSettings["EmailCC"];
                if (!string.IsNullOrEmpty(ccMailsString))
                {
                    foreach (var mailAddress in ccMailsString.Split(',').Select(x => new MailAddress(x)).ToList())
                    {
                        mess.CC.Add(mailAddress);
                    }
                }
                
                smtp.Send(mess);
                
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(typeof(ContactSurfaceController), e, "Error sending contact form.");
                return false;
            }
        }
    }
}