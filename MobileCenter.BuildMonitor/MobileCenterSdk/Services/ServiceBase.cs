using MobileCenterSdk.Models;
using MobileCenterSdk.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileCenterSdk.Services
{
    public abstract class ServiceBase
    {
        MobileCenterSdkClient _mcsc;

        protected ServiceBase(MobileCenterSdkClient mcsc)
        {
            _mcsc = mcsc;
        }

        protected virtual HttpClient GetClient()
        {
            return new HttpClient();
        }
        
        protected virtual HttpRequestMessage PrepareHttpRequest(string endpoint, HttpMethod method, List<KeyValuePair<string, string>> queryParameters = null, object body = null, string basicAuthUsername = "", string basicAuthPassword = "")
        {
            var url = $"{ApiSettings.ApiBaseUrl}/{endpoint}";
            if (queryParameters != null && queryParameters.Count > 0)
            {
                using (var content = new FormUrlEncodedContent(queryParameters))
                {
                    var query = content.ReadAsStringAsync().Result;
                    url += $"?{query}";
                }
            }

            var request = new HttpRequestMessage(method, url);

            if(body != null)
            {
                var jsonbody = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonbody, Encoding.UTF8, "application/json");
            }else if (method == HttpMethod.Post)
            {
                request.Content = new StringContent("", Encoding.UTF8, "application/json");
            }
            var authHeader = _mcsc.Credentials.AuthHeader();
            request.Headers.Add(authHeader.Key, authHeader.Value);

            return request;
        }
        protected async Task<T> SendRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            response = await GetClient().SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var obj = JsonConvert.DeserializeObject<T>(json);
                return AttachServices<T>(obj);
            }
            else
            {
                var json = string.Empty;
                try
                {
                    json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    throw new Exception("Something went horribly wrong in System.Net.Http");
                }
                catch (HttpRequestException e)
                {

                    throw HandleHttpRequestException(e, json, response.StatusCode);
                }
            }
            
        }
        protected async Task SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            response = await GetClient().SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var json = string.Empty;
                try
                {
                    json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    throw new Exception("Something went horribly wrong in System.Net.Http");
                }
                catch (HttpRequestException e)
                {

                    throw HandleHttpRequestException(e, json, response.StatusCode);
                }
            }
        }
        private T AttachServices<T>(T obj)
        {
            if (obj is IAccountServiceHolder)
            {
                (obj as IAccountServiceHolder).AccountService = _mcsc.AccountService;
            }
            if (obj is IBuildServiceHolder)
            {
                (obj as IBuildServiceHolder).BuildService = _mcsc.BuildService;
            }
            return obj;
        }
        protected Exception HandleHttpRequestException(Exception e, string json, HttpStatusCode statusCode)
        {
            try
            {
                var status = JsonConvert.DeserializeObject<McStatus>(json);
                return new MobileCenterException(status, statusCode);
            }catch
            {
                throw e;
            }
        }
    }
}
