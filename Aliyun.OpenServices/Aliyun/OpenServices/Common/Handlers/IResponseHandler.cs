namespace Aliyun.OpenServices.Common.Handlers
{
    using Aliyun.OpenServices.Common.Communication;
    using System;

    internal interface IResponseHandler
    {
        void Handle(ServiceResponse response);
    }
}

