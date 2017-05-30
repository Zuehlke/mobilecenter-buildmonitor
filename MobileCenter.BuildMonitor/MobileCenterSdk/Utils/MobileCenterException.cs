using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{ 
    [Serializable]
    public class MobileCenterException : Exception
    {
        public McStatus MobileCenterRequestStatus { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public MobileCenterException() { }
        public MobileCenterException(string message) : base(message) { }
        public MobileCenterException(string message, Exception innerException) : base(message, innerException) { }
        public MobileCenterException(McStatus status, HttpStatusCode httpStatusCode) : base($"{httpStatusCode}: {status.Error.Message}")
        {
            MobileCenterRequestStatus = status;
            StatusCode = httpStatusCode;
        }
    }
}
