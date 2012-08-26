namespace Aliyun.OpenServices.OpenStorageService
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class OssObjectSummary
    {
        internal OssObjectSummary()
        {
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "[OSSObjectSummary BucketName={0}, Key={1}]", new object[] { this.BucketName, this.Key });
        }

        public string BucketName { get; internal set; }

        public string ETag { get; internal set; }

        public string Key { get; internal set; }

        public DateTime LastModified { get; internal set; }

        public Aliyun.OpenServices.OpenStorageService.Owner Owner { get; internal set; }

        public long Size { get; internal set; }

        public string StorageClass { get; internal set; }
    }
}

