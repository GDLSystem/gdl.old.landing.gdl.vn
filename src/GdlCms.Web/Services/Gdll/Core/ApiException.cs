using System;

namespace GdlCms.Web.Services.Gdll.Core
{
    public class ApiException<T> : Exception
    { 
        public ApiResponse<T> Response { get; set; }

        public ApiException(ApiResponse<T> response, string message)
            : base(message)
        {
            Response = response;
        }
    }
}