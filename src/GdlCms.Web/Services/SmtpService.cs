using System;
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
        
        public bool SendEmail(string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage
                {
                    IsBodyHtml = true
                };
                SmtpClient client = new SmtpClient();

                string toAddress = "kynhero@gmail.com";
                string fromAddress = "kynhero@gmail.com";
                // message.Subject =  $"[GDL-CMS] [NEW CONTACT]: {model.EmailTitle} - {model.Name}";
                message.Subject = subject;
                message.Body = body;
                message.To.Add(new MailAddress(toAddress, toAddress));
                message.From = new MailAddress(fromAddress, fromAddress);
                
                client.Send(message);
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