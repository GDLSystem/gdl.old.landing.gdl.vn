using GdlCms.Web.ViewModels;

namespace GdlCms.Web.Services
{
    public interface ISmtpService
    {
        bool SendEmail(string inputEmail,string phone, string name,string bodyEmail);
    }
}