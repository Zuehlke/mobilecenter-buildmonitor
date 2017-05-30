using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToRepoStateTypeConverter
    {
        private const string Unauthorized = "unauthorized";
        private const string Inactive = "inactive";
        private const string Active = "active";
        public static McRepoState Convert(string repoStateString)
        {
            switch(repoStateString)
            {
                case Unauthorized:
                    return McRepoState.Unauthorized;
                case Inactive:
                    return McRepoState.Inactive;
                case Active:
                    return McRepoState.Active;
                default:
                    return McRepoState.Unknown;
            }
        }
        public static string ConvertBack(McRepoState repoState)
        {
            switch(repoState)
            {
                case McRepoState.Unauthorized:
                    return Unauthorized;
                case McRepoState.Inactive:
                    return Inactive;
                case McRepoState.Active:
                    return Active;
                default:
                    return string.Empty;
            }
        }
    }
}
