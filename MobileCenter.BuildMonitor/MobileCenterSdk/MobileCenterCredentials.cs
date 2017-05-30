using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk
{
    public class MobileCenterCredentials
    {
        public string _apiKey, _username, _password;
        public bool _isApiKey;
        public MobileCenterCredentials(string apiKey)
        {
            _isApiKey = true;
            _apiKey = apiKey;
        }
        public MobileCenterCredentials(string username, string password)
        {
            _isApiKey = false;
            _username = username;
            _password = password;
        }
        public KeyValuePair<string, string> AuthHeader()
        {
            if (_isApiKey)
            {
                return new KeyValuePair<string, string>("X-API-Token", _apiKey);
            }
            else
            {
                var authContent = Encoding.UTF8.GetBytes($"{_username}:{_password}");
                var authToken = Convert.ToBase64String(authContent);
                return new KeyValuePair<string, string>("Authorization", $"Basic {authToken}");
            }
        }
    }
}
