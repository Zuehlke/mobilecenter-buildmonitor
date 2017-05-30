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
            var tokenInfo = await MobileCenterClient.AccountService.CreateApiTokenAsync(
                new McApiTokenInformation()
                {
                    Description = "Mobile Center Build Monitor",
                    Scope = new List<string> { "all" }
                });
            ServiceLocator.SettingsService.Token = tokenInfo.Token;
            MobileCenterClient.Credentials = new MobileCenterCredentials(tokenInfo.Token);

        }
    }
}

