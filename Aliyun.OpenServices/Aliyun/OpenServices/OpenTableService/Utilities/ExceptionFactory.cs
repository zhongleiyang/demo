namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Model;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    internal static class ExceptionFactory
    {
        public static OtsException CreateException(ErrorResult errorResult, Exception innerException)
        {
            return CreateException(errorResult.Code, errorResult.Message, errorResult.RequestId, errorResult.HostId, innerException);
        }

        public static OtsException CreateException(string errorCode, string message, string requestId, string hostId)
        {
            return CreateException(errorCode, message, requestId, hostId, null);
        }

        public static OtsException CreateException(string errorCode, string message, string requestId, string hostId, Exception innerException)
        {
            OtsException exception = (innerException != null) ? new OtsException(message, innerException) : new OtsException(message);
            exception.ErrorCode = errorCode;
            exception.RequestId = requestId;
            exception.HostId = hostId;
            return exception;
        }

        public static Exception CreateInvalidResponseException(ServiceResponse response, string message, Exception innerException)
        {
            string requestId = null;
            string hostId = null;
            if (response != null)
            {
                try
                {
                    string responseString = ReadResponseAsString(response);
                    Match requestIdMatch = new Regex(@"\<RequestID\>(\w+)\</RequestID\>").Match(responseString);
                    if (requestIdMatch.Success)
                    {
                        requestId = requestIdMatch.Groups[1].Value;
                    }
                    Match hostIdMatch = new Regex(@"\<HostID\>(\w+)\</HostID\>").Match(responseString);
                    if (hostIdMatch.Success)
                    {
                        hostId = hostIdMatch.Groups[1].Value;
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
            if (!string.IsNullOrEmpty(requestId) || !string.IsNullOrEmpty(hostId))
            {
                message = message + string.Format(CultureInfo.InvariantCulture, OtsExceptions.InvalidResponseMessage, new object[] { requestId, hostId });
            }
            return new InvalidOperationException(message, innerException);
        }

        private static string ReadResponseAsString(ServiceResponse response)
        {
            using (Stream responseStream = response.Content)
            {
                StringBuilder stringBuilder = new StringBuilder();
                byte[] buffer = new byte[0x1000];
                while (responseStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    string bufString = OtsUtility.DataEncoding.GetString(buffer);
                    stringBuilder.Append(bufString);
                }
                return stringBuilder.ToString();
            }
        }
    }
}

