namespace Aliyun.OpenServices.OpenStorageService
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ResponseHeaderOverrides
    {
        internal const string ResponseCacheControl = "response-cache-control";
        internal const string ResponseContentDisposition = "response-content-disposition";
        internal const string ResponseContentEncoding = "response-content-encoding";
        internal const string ResponseHeaderContentLanguage = "response-content-language";
        internal const string ResponseHeaderContentType = "response-content-type";
        internal const string ResponseHeaderExpires = "response-expires";

        internal void Populate(IDictionary<string, string> parameters)
        {
            if (this.CacheControl != null)
            {
                parameters.Add("response-cache-control", this.CacheControl);
            }
            if (this.ContentDisposition != null)
            {
                parameters.Add("response-content-disposition", this.ContentDisposition);
            }
            if (this.ContentEncoding != null)
            {
                parameters.Add("response-content-encoding", this.ContentEncoding);
            }
            if (this.ContentLanguage != null)
            {
                parameters.Add("response-content-language", this.ContentLanguage);
            }
            if (this.ContentType != null)
            {
                parameters.Add("response-content-type", this.ContentType);
            }
            if (this.Expires != null)
            {
                parameters.Add("response-expires", this.Expires);
            }
        }

        public string CacheControl { get; set; }

        public string ContentDisposition { get; set; }

        public string ContentEncoding { get; set; }

        public string ContentLanguage { get; set; }

        public string ContentType { get; set; }

        public string Expires { get; set; }
    }
}

