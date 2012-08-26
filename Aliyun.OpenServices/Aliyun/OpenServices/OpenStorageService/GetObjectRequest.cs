namespace Aliyun.OpenServices.OpenStorageService
{
    using Aliyun.OpenServices.Common.Utilities;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class GetObjectRequest
    {
        private IList<string> _matchingETagConstraints = new List<string>();
        private IList<string> _nonmatchingEtagConstraints = new List<string>();
        private ResponseHeaderOverrides _responseHeaders = new ResponseHeaderOverrides();

        public GetObjectRequest(string bucketName, string key)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "bucketName");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "key");
            }
            if (!OssUtils.IsBucketNameValid(bucketName))
            {
                throw new ArgumentException(OssResources.BucketNameInvalid, "bucketName");
            }
            if (!OssUtils.IsObjectKeyValid(key))
            {
                throw new ArgumentException(OssResources.ObjectKeyInvalid, "key");
            }
            this.BucketName = bucketName;
            this.Key = key;
        }

        private static string JoinETag(IEnumerable<string> etags)
        {
            StringBuilder result = new StringBuilder();
            bool first = true;
            foreach (string etag in etags)
            {
                if (!first)
                {
                    result.Append(", ");
                }
                result.Append(etag);
                first = false;
            }
            return result.ToString();
        }

        internal void Populate(IDictionary<string, string> headers)
        {
            if (this.Range != null)
            {
                headers.Add("Range", string.Format(CultureInfo.InvariantCulture, "bytes={0}-{1}", new object[] { this.Range[0], this.Range[1] }));
            }
            if (this.ModifiedSinceConstraint.HasValue)
            {
                headers.Add("If-Modified-Since", DateUtils.FormatRfc822Date(this.ModifiedSinceConstraint.Value));
            }
            if (this.UnmodifiedSinceConstraint.HasValue)
            {
                headers.Add("If-Unmodified-Since", DateUtils.FormatRfc822Date(this.UnmodifiedSinceConstraint.Value));
            }
            if (this._matchingETagConstraints.Count > 0)
            {
                headers.Add("If-Match", JoinETag(this._matchingETagConstraints));
            }
            if (this._nonmatchingEtagConstraints.Count > 0)
            {
                headers.Add("If-None-Match", JoinETag(this._nonmatchingEtagConstraints));
            }
        }

        public void SetRange(long start, long end)
        {
            this.Range = new long[] { start, end };
        }

        public string BucketName { get; private set; }

        public string Key { get; private set; }

        public IList<string> MatchingETagConstraints
        {
            get
            {
                return this._matchingETagConstraints;
            }
        }

        public DateTime? ModifiedSinceConstraint { get; set; }

        public IList<string> NonmatchingETagConstraints
        {
            get
            {
                return this._nonmatchingEtagConstraints;
            }
        }

        public long[] Range { get; private set; }

        public ResponseHeaderOverrides ResponseHeaders
        {
            get
            {
                return this._responseHeaders;
            }
        }

        public DateTime? UnmodifiedSinceConstraint { get; set; }
    }
}

