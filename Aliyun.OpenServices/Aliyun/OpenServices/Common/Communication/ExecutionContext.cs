namespace Aliyun.OpenServices.Common.Communication
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Authentication;
    using Aliyun.OpenServices.Common.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ExecutionContext
    {
        private IList<IResponseHandler> _responseHandlers = new List<IResponseHandler>();
        private const string DefaultEncoding = "utf-8";

        public ExecutionContext()
        {
            this.Charset = "utf-8";
        }

        public string Charset { get; set; }

        public ServiceCredentials Credentials { get; set; }

        public IList<IResponseHandler> ResponseHandlers
        {
            get
            {
                return this._responseHandlers;
            }
        }

        public IRequestSigner Signer { get; set; }
    }
}

