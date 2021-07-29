using System.Net;  
using System.Net.Mail;  
namespace SendMail {  
    class Program {  
        static string smtpAddress = "smtp.gmail.com";  
        static int portNumber = 587;  
        static bool enableSSL = true;  
        static string emailFromAddress = "kynhero@gmail.com"; //Sender Email Address  
        static string password = ""; //Sender Password  
        static string emailToAddress = "kynhero@gmail.com"; //Receiver Email Address  
        static string subject = "Hello";  
        static string body = "Hello, This is Email sending test using gmail.";  
        static void Main(string[] args) {  
            SendEmail();  
        }  
        public static void SendEmail() {  
            using(MailMessage mail = new MailMessage()) {  
                mail.From = new MailAddress(emailFromAddress);  
                mail.To.Add(emailToAddress);  
                mail.Subject = subject;  
                mail.Body = body;  
                mail.IsBodyHtml = true;  
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using(SmtpClient smtp = new SmtpClient(smtpAddress, portNumber)) {  
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);  
                    smtp.EnableSsl = enableSSL;  
                    smtp.Send(mail);  
                }  
            }  
        }  
    }  
}