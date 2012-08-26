namespace Aliyun.OpenServices.OpenStorageService.Utilities
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Authentication;
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ExecutionContextBuilder
    {
        public ExecutionContextBuilder()
        {
            this.ResponseHandlers = new List<IResponseHandler>();
        }

        public ExecutionContext Build()
        {
            ExecutionContext context = new ExecutionContext {
                Signer = CreateSigner(this.Bucket, this.Key),
                Credentials = this.Credentials
            };
            foreach (IResponseHandler h in this.ResponseHandlers)
            {
                context.ResponseHandlers.Add(h);
            }
            return context;
        }

        private static IRequestSigner CreateSigner(string bucket, string key)
        {
            return new OssRequestSigner("/" + ((bucket != null) ? bucket : "") + ((key != null) ? ("/" + key) : ""));
        }

        public string Bucket { get; set; }

        public ServiceCredentials Credentials { get; set; }

        public string Key { get; set; }

        public HttpMethod Method { get; set; }

        public IList<IResponseHandler> ResponseHandlers { get; private set; }
    }
}

