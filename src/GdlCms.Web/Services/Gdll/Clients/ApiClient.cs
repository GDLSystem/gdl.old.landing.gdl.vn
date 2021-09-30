using System;
using System.Configuration;
using System.Security.Authentication;
using GdlCms.Web.Services.Gdll.Consumers;
using GdlCms.Web.Services.Gdll.Entities;
using GdlCms.Web.Services.Gdll.Helpers;
using RestSharp;

namespace GdlCms.Web.Services.Gdll.Clients
{
    public interface IApiClient
    {
        
    }
    
    public class ApiClient
    {
        private readonly RestClient _client;
        private readonly RestClient _authClient;

        private TokenModel.Response _accessToken;

        private const string AuthError = "Please authenticate before attempting to use the API";
        
        private string _apiUrl;
        private string _authUrl;

        public ApiClient()
        {
            _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            _authUrl = ConfigurationManager.AppSettings["AuthUrl"];

// #if DEBUG
//             _apiUrl = "https://localhost:44344";
//             _authUrl = "https://localhost:44382";
// #endif
            _authClient = RestHelper.CreateClient(_authUrl);
            _client = RestHelper.CreateClient(_apiUrl);
            
            _auth = new AuthApiConsumer(_authClient);
            Auth();
            
            _group = new GroupApiConsumer(_client);
        }
        
        private readonly AuthApiConsumer _auth;
        private readonly GroupApiConsumer _group;

        private void Auth()
        {
            var tokenResponse = _auth.PostTokenDefault();
            if (tokenResponse.IsSuccess)
            {
                _accessToken = tokenResponse.Resource;
                SetAuthHeader(_accessToken.access_token);
            }
        }
        
        private void SetAuthHeader(string token)
        {
            try
            {
                // lib exception: sequence contains more than 1 element
                _client.RemoveDefaultParameter("Authorization");
            }
            catch (Exception)
            {
                // ignored
            }

            _client.AddDefaultHeader("Authorization", $"Bearer {token}");
        }

        public GroupApiConsumer Group
        {
            get
            {
                if (_accessToken == null)
                {
                    throw new AuthenticationException(AuthError);
                }

                return _group;
            }
        }
        
    }
}