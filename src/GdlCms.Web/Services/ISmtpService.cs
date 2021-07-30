using GdlCms.Web.ViewModels;

namespace GdlCms.Web.Services
{
    public interface ISmtpService
    {
        bool SendEmail(string subject, string body);
    }
}