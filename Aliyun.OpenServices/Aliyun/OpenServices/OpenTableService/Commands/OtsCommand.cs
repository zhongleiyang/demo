namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal abstract class OtsCommand
    {
        public const string UrlEncodedContentType = "application/x-www-form-urlencoded; charset=utf-8";

        protected OtsCommand(IServiceClient client, Uri endpoint, ExecutionContext context)
        {
            this.Endpoint = endpoint;
            this.Client = client;
            this.Context = context;
        }

        protected virtual void AddRequestParameters(IDictionary<string, string> parameters)
        {
        }

        public IAsyncResult BeginExecute(AsyncCallback callback, object state)
        {
            return this.Client.BeginSend(this.CreateRequest(), this.Context, callback, state);
        }

        protected ServiceRequest CreateRequest()
        {
            ServiceRequest request = new ServiceRequest {
                Method = this.Method,
                Endpoint = this.Endpoint,
                ResourcePath = this.ResourcePath
            };
            request.Headers["Content-Type"] = "application/x-www-form-urlencoded; charset=utf-8";
            this.AddRequestParameters(request.Parameters);
            return request;
        }

        public static void EndExecute(IServiceClient client, IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                throw new ArgumentNullException("asyncResult");
            }
            using (client.EndSend(asyncResult))
            {
            }
        }

        public void Execute()
        {
            using (this.Client.Send(this.CreateRequest(), this.Context))
            {
            }
        }

        public IServiceClient Client { get; private set; }

        public ExecutionContext Context { get; private set; }

        public Uri Endpoint { get; private set; }

        protected abstract HttpMethod Method { get; }

        protected abstract string ResourcePath { get; }
    }
}

