namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Authentication;
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal class OtsRequestSigner : IRequestSigner
    {
        private static void AddRequiredParameters(IDictionary<string, string> parameters, string action, ServiceCredentials credentials, ServiceSignature signer, DateTime timestamp)
        {
            if (parameters.ContainsKey("Signature"))
            {
                parameters.Remove("Signature");
            }
            parameters["Date"] = DateUtils.FormatRfc822Date(timestamp);
            parameters["OTSAccessKeyId"] = credentials.AccessId;
            parameters["APIVersion"] = "1";
            parameters["SignatureMethod"] = signer.SignatureMethod;
            parameters["SignatureVersion"] = signer.SignatureVersion;
            string signature = GetSignature(credentials, signer, action, parameters);
            parameters["Signature"] = signature;
        }

        private static string GetSignature(ServiceCredentials credentials, ServiceSignature signer, string action, IDictionary<string, string> parameters)
        {
            object[] temp = new object[] { action, HttpUtils.GetRequestParameterString(Enumerable.OrderBy<KeyValuePair<string, string>, string>(parameters, (Func<KeyValuePair<string, string>, string>) (e => e.Key), StringComparer.Ordinal)) };
            string signatureData = string.Format(CultureInfo.InvariantCulture, "/{0}\n{1}", temp);
            return signer.ComputeSignature(credentials.AccessKey, signatureData);
        }

        public void Sign(ServiceRequest request, ServiceCredentials credentials)
        {
            AddRequiredParameters(request.Parameters, request.ResourcePath, credentials, ServiceSignature.Create(), DateTime.UtcNow);
        }
    }
}

