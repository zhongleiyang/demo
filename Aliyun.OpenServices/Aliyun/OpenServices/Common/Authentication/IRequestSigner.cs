namespace Aliyun.OpenServices.Common.Authentication
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Communication;
    using System;

    internal interface IRequestSigner
    {
        void Sign(ServiceRequest request, ServiceCredentials credentials);
    }
}

