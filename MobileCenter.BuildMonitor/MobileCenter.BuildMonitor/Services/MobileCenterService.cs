using MobileCenterSdk;
using MobileCenterSdk.Models;
using MobileCenterSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenter.BuildMonitor.Services
{
    public class MobileCenterService
    {
        private readonly string _tokenDescription = "Mobile Center Build Monitor";
        private readonly string _tokenScope = "all";

        public MobileCenterSdkClient _mobileCenterClient;
        public MobileCenterSdkClient MobileCenterClient
        {
            get
            {
                return _mobileCenterClient ?? (_mobileCenterClient = new MobileCenterSdkClient());
            }
        }

        public static MobileCenterCredentials MobileCenterCreds { get; private set; }

        public async Task LoginAsync(string username, string password)
        {
            MobileCenterClient.Credentials = new MobileCenterCredentials(username, password);
            var tokens = await MobileCenterClient.AccountService.GetApiTokensAsync();
            if(tokens.Count > 0)
            {
                var matchingToken = tokens.SingleOrDefault(x => x.Description == _tokenDescription && x.Scope.Contains(_tokenScope));
                if (matchingToken != null)
                {
                    await MobileCenterClient.AccountService.DeleteApiTokenAsync(matchingToken.Id);
                }
            }
            var tokenInfo = await MobileCenterClient.AccountService.CreateApiTokenAsync(
                new McApiTokenInformation()
                {
                    Description = _tokenDescription,
                    Scope = new List<string> { _tokenScope }
                });
            
            ServiceLocator.SettingsService.Token = tokenInfo.Token;
            MobileCenterClient.Credentials = new MobileCenterCredentials(tokenInfo.Token);
        }
    }
}

