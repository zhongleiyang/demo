namespace Aliyun.OpenServices.Common.Communication
{
    using Aliyun.OpenServices.Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class ServiceRequest : ServiceMessage
    {
        private IDictionary<string, string> parameters = new Dictionary<string, string>();

        public Stream BuildRequestContent()
        {
            if (!this.IsParameterInUri())
            {
                string paramString = HttpUtils.GetRequestParameterString(this.parameters);
                if (!string.IsNullOrEmpty(paramString))
                {
                    byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(paramString);
                    Stream content = new MemoryStream();
                    content.Write(buffer, 0, buffer.Length);
                    content.Flush();
                    content.Seek(0, SeekOrigin.Begin);
                    return content;
                }
            }
            return this.Content;
        }

        public string BuildRequestUri()
        {
            string uri = this.Endpoint.ToString();
            if (!uri.EndsWith("/") && ((this.ResourcePath == null) || !this.ResourcePath.StartsWith("/")))
            {
                uri = uri + "/";
            }
            if (this.ResourcePath != null)
            {
                uri = uri + this.ResourcePath;
            }
            if (this.IsParameterInUri())
            {
                string paramString = HttpUtils.GetRequestParameterString(this.parameters);
                if (!string.IsNullOrEmpty(paramString))
                {
                    uri = uri + "?" + paramString;
                }
            }
            return uri;
        }

        private bool IsParameterInUri()
        {
            bool requestHasNoPayload = this.Content != null;
            return ((this.Method != HttpMethod.Post) || requestHasNoPayload);
        }

        public Uri Endpoint { get; set; }

        public HttpMethod Method { get; set; }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public string ResourcePath { get; set; }
    }
}

