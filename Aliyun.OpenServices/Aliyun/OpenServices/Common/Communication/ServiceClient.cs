namespace Aliyun.OpenServices.Common.Communication
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common;
    using Aliyun.OpenServices.Common.Handlers;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;

    internal abstract class ServiceClient : IServiceClient
    {
        private ClientConfiguration _configuration;

        protected ServiceClient(ClientConfiguration configuration)
        {
            this._configuration = (ClientConfiguration) configuration.Clone();
        }

        public IAsyncResult BeginSend(ServiceRequest request, ExecutionContext context, AsyncCallback callback, object state)
        {
            SignRequest(request, context);
            return this.BeginSendCore(request, context, callback, state);
        }

        protected abstract IAsyncResult BeginSendCore(ServiceRequest request, ExecutionContext context, AsyncCallback callback, object state);
        public static ServiceClient Create(ClientConfiguration configuration)
        {
            return new ServiceClientImpl(configuration);
        }

        public ServiceResponse EndSend(IAsyncResult aysncResult)
        {
            ServiceResponse temp;
            AsyncResult<ServiceResponse> ar = aysncResult as AsyncResult<ServiceResponse>;
            try
            {
                ServiceResponse result = ar.GetResult();
                ar.Dispose();
                temp = result;
            }
            catch (ObjectDisposedException)
            {
                throw new InvalidOperationException(Resources.ExceptionEndOperationHasBeenCalled);
            }
            return temp;
        }

        protected static void HandleResponse(ServiceResponse response, IList<IResponseHandler> handlers)
        {
            foreach (IResponseHandler handler in handlers)
            {
                handler.Handle(response);
            }
        }

        public ServiceResponse Send(ServiceRequest request, ExecutionContext context)
        {
            SignRequest(request, context);
            ServiceResponse response = this.SendCore(request, context);
            HandleResponse(response, context.ResponseHandlers);
            return response;
        }

        protected abstract ServiceResponse SendCore(ServiceRequest request, ExecutionContext context);
        private static void SignRequest(ServiceRequest request, ExecutionContext context)
        {
            if (context.Signer != null)
            {
                context.Signer.Sign(request, context.Credentials);
            }
        }

        protected ClientConfiguration Configuration
        {
            get
            {
                return this._configuration;
            }
        }
    }
}

