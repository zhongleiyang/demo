namespace Aliyun.OpenServices.Common.Communication
{
    using System;

    internal interface IServiceClient
    {
        IAsyncResult BeginSend(ServiceRequest request, ExecutionContext context, AsyncCallback callback, object state);
        ServiceResponse EndSend(IAsyncResult asyncResult);
        ServiceResponse Send(ServiceRequest request, ExecutionContext context);
    }
}

