namespace Aliyun.OpenServices.OpenStorageService.Utilities
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Communication;
    using System;

    internal class ServiceClientFactory
    {
        public static IServiceClient CreateServiceClient(ClientConfiguration configuration)
        {
            return new RetryableServiceClient(ServiceClient.Create(configuration)) { MaxErrorRetry = configuration.MaxErrorRetry };
        }
    }
}

