namespace Aliyun.OpenServices.Common.Communication
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Net;

    internal static class HttpFactory
    {
        internal static HttpWebRequest CreateWebRequest(ServiceRequest serviceRequest, ClientConfiguration configuration)
        {
            HttpWebRequest webRequest = WebRequest.Create(serviceRequest.BuildRequestUri()) as HttpWebRequest;
            SetRequestHeaders(webRequest, serviceRequest, configuration);
            SetRequestProxy(webRequest, configuration);
            return webRequest;
        }

        private static void SetRequestHeaders(HttpWebRequest webRequest, ServiceRequest serviceRequest, ClientConfiguration configuration)
        {
            webRequest.Timeout = configuration.ConnectionTimeout;
            webRequest.Method = serviceRequest.Method.ToString().ToUpperInvariant();
            foreach (KeyValuePair<string, string> h in serviceRequest.Headers)
            {
                webRequest.Headers.AddInternal(h.Key, HttpUtils.ReEncode(h.Value, "utf-8", "iso-8859-1"));
            }
            if (!string.IsNullOrEmpty(configuration.UserAgent))
            {
                webRequest.UserAgent = configuration.UserAgent;
            }
        }

        private static void SetRequestProxy(HttpWebRequest webRequest, ClientConfiguration configuration)
        {
            if (!string.IsNullOrEmpty(configuration.ProxyHost))
            {
                if (configuration.ProxyPort < 0)
                {
                    webRequest.Proxy = new WebProxy(configuration.ProxyHost);
                }
                else
                {
                    webRequest.Proxy = new WebProxy(configuration.ProxyHost, configuration.ProxyPort);
                }
                if (!string.IsNullOrEmpty(configuration.ProxyUserName))
                {
                    webRequest.Proxy.Credentials = string.IsNullOrEmpty(configuration.ProxyDomain) ? new NetworkCredential(configuration.ProxyUserName, configuration.ProxyPassword ?? string.Empty) : new NetworkCredential(configuration.ProxyUserName, configuration.ProxyPassword ?? string.Empty, configuration.ProxyDomain);
                }
            }
        }
    }
}

