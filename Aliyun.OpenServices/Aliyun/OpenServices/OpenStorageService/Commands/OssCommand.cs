namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal abstract class OssCommand
    {
        public OssCommand(IServiceClient client, Uri endpoint, ExecutionContext context)
        {
            this.Endpoint = endpoint;
            this.Client = client;
            this.Context = context;
        }

        private ServiceRequest BuildRequest()
        {
            ServiceRequest request = new ServiceRequest {
                Method = this.Method,
                Endpoint = this.Endpoint,
                ResourcePath = this.ResourcePath
            };
            foreach (KeyValuePair<string, string> p in this.Parameters)
            {
                request.Parameters.Add(p.Key, p.Value);
            }
            request.Headers["Date"] = DateUtils.FormatRfc822Date(DateTime.UtcNow);
            if (!this.Headers.ContainsKey("Content-Type"))
            {
                request.Headers["Content-Type"] = string.Empty;
            }
            foreach (KeyValuePair<string, string> h in this.Headers)
            {
                request.Headers.Add(h.Key, h.Value);
            }
            request.Content = this.Content;
            return request;
        }

        public ServiceResponse Execute()
        {
            return this.Client.Send(this.BuildRequest(), this.Context);
        }

        public IServiceClient Client { get; private set; }

        protected virtual Stream Content
        {
            get
            {
                return null;
            }
        }

        public ExecutionContext Context { get; private set; }

        public Uri Endpoint { get; private set; }

        protected virtual IDictionary<string, string> Headers
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }

        protected virtual HttpMethod Method
        {
            get
            {
                return HttpMethod.Get;
            }
        }

        protected virtual IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }

        protected abstract string ResourcePath { get; }
    }
}

