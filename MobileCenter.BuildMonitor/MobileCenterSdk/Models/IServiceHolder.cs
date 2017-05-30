using MobileCenterSdk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    internal interface IAccountServiceHolder
    {
        AccountService AccountService { get; set; }
    }
    internal interface IBuildServiceHolder
    {
        BuildService BuildService { get; set; }
    }
}
