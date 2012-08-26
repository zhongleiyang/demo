namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using System;

    internal class ServiceClientFactory
    {
        private static bool CanRetry(Exception ex)
        {
            OtsException otsException = ex as OtsException;
            if (otsException == null)
            {
                return false;
            }
            if ((!(otsException.ErrorCode == "OTSInternalServerError") && !(otsException.ErrorCode == "OTSStorageServerBusy")) && (!(otsException.ErrorCode == "OTSStorageTimeout") && !(otsException.ErrorCode == "OTSStorageTxnLockKeyFail")))
            {
                return (otsException.ErrorCode == "OTSStoragePartitionNotReady");
            }
            return true;
        }

        public static IServiceClient CreateServiceClient(ClientConfiguration configuration)
        {
            return new RetryableServiceClient(ServiceClient.Create(configuration)) { MaxErrorRetry = configuration.MaxErrorRetry, ShouldRetryCallback = new Func<Exception, bool>(ServiceClientFactory.CanRetry) };
        }
    }
}

