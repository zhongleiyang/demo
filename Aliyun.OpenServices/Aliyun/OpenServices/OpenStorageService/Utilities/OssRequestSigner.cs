namespace Aliyun.OpenServices.OpenStorageService.Utilities
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Authentication;
    using Aliyun.OpenServices.Common.Communication;
    using System;

    internal class OssRequestSigner : IRequestSigner
    {
        private string _resourcePath;

        public OssRequestSigner(string resourcePath)
        {
            this._resourcePath = resourcePath;
        }

        public void Sign(ServiceRequest request, ServiceCredentials credentials)
        {
            string accessKeyId = credentials.AccessId;
            string secretAccessKey = credentials.AccessKey;
            string httpMethod = request.Method.ToString().ToUpperInvariant();
            string resourcePath = this._resourcePath;
            if (!string.IsNullOrEmpty(secretAccessKey))
            {
                string canonicalString = SignUtils.BuildCanonicalString(httpMethod, resourcePath, request);
                string signature = ServiceSignature.Create().ComputeSignature(secretAccessKey, canonicalString);
                request.Headers.Add("Authorization", "OSS " + accessKeyId + ":" + signature);
            }
            else
            {
                request.Headers.Add("Authorization", accessKeyId);
            }
        }
    }
}

