using System;

namespace GdlCms.Web.Services.Gdll.Core
{
    public interface IErrorLogger
    {
        void LogError(Exception ex, string infoMessage);
    }
    
    public class ErrorLogger : IErrorLogger
    {
        public void LogError(Exception ex, string infoMessage)
        {
            //Log the error to your error database
        }
       
    }
}