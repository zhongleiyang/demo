namespace Aliyun.OpenServices.OpenStorageService
{
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ObjectListing
    {
        private IList<string> _commonPrefixes = new List<string>();
        private IList<OssObjectSummary> _objectSummaries = new List<OssObjectSummary>();

        internal ObjectListing(string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "bucketName");
            }
            if (!OssUtils.IsBucketNameValid(bucketName))
            {
                throw new ArgumentException(OssResources.BucketNameInvalid, "bucketName");
            }
            this.BucketName = bucketName;
        }

        internal void AddCommonPrefix(string prefix)
        {
            this._commonPrefixes.Add(prefix);
        }

        internal void AddObjectSummary(OssObjectSummary summary)
        {
            this._objectSummaries.Add(summary);
        }

        public string BucketName { get; private set; }

        public IEnumerable<string> CommonPrefixes
        {
            get
            {
                return this._commonPrefixes;
            }
        }

        public string Delimiter { get; internal set; }

        public bool IsTrunked { get; internal set; }

        public string Marker { get; internal set; }

        public int MaxKeys { get; internal set; }

        public string NextMarker { get; internal set; }

        public IEnumerable<OssObjectSummary> ObjectSummaries
        {
            get
            {
                return this._objectSummaries;
            }
        }

        public string Prefix { get; internal set; }
    }
}

