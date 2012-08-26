namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Authentication;
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Handlers;
    using Aliyun.OpenServices.Common.Utilities;
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    internal class ValidationResponseHandler : ResponseHandler
    {
        private string _action;
        private ServiceCredentials _credentials;
        private const int _responseTimeoutMinutes = 15;

        public ValidationResponseHandler(ServiceCredentials credentials, string action)
        {
            this._credentials = credentials;
            this._action = action;
        }

        public override void Handle(ServiceResponse response)
        {
            base.Handle(response);
            IDictionary<string, string> headers = response.Headers;
            if (!headers.Keys.Contains("x-ots-date"))
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ResponseDoesNotContainHeader, new object[] { "x-ots-date" }), null);
            }
            if (!headers.Keys.Contains("Content-Md5"))
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ResponseDoesNotContainHeader, new object[] { "Content-Md5" }), null);
            }
            if (!headers.Keys.Contains("Content-Type"))
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, string.Format(CultureInfo.CurrentUICulture, OtsExceptions.ResponseDoesNotContainHeader, new object[] { "Content-Type" }), null);
            }
            if (!headers.Keys.Contains("Authorization"))
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, OtsExceptions.ResponseFailedAuthorization, null);
            }
            string otsDate = headers["x-ots-date"];
            if (string.IsNullOrEmpty(otsDate))
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, OtsExceptions.ResponseExpired, null);
            }
            DateTime responseTime = DateUtils.ParseRfc822Date(otsDate);
            if (DateTime.UtcNow.Subtract(responseTime).TotalMinutes > 15.0)
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, OtsExceptions.ResponseExpired, null);
            }
            StringBuilder canonicalizedOtsHeader = new StringBuilder();
            foreach (string key in headers.Keys)
            {
                if (key.StartsWith("x-ots-", StringComparison.OrdinalIgnoreCase))
                {
                    canonicalizedOtsHeader.AppendFormat(CultureInfo.InvariantCulture, "{0}:{1}\n", new object[] { key, headers[key] });
                }
            }
            string contentMd5 = headers["Content-Md5"];
            string contentType = headers["Content-Type"];
            string canonicalizedResource = "/" + this._action;
            string data = contentMd5 + "\n" + contentType + "\n" + canonicalizedOtsHeader.ToString() + canonicalizedResource;
            string actual = ServiceSignature.Create().ComputeSignature(this._credentials.AccessKey, data);
            string authorizationHeader = headers["Authorization"];
            bool authEqual = false;
            if (authorizationHeader.Contains<char>(':'))
            {
                authEqual = authorizationHeader.Split(new char[] { ':' }).Last<string>().EndsWith(actual, StringComparison.Ordinal);
            }
            if (!authEqual)
            {
                throw ExceptionFactory.CreateInvalidResponseException(response, OtsExceptions.ResponseFailedAuthorization, null);
            }
        }
    }
}

