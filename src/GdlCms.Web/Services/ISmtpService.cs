using GdlCms.Web.ViewModels;

namespace GdlCms.Web.Services
{
    public interface ISmtpService
    {
        bool SendEmail(string inputEmail, string name,string body);
    }
}