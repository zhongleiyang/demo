namespace Aliyun.OpenServices.OpenStorageService
{
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Runtime.CompilerServices;

    public class ListObjectsRequest
    {
        public ListObjectsRequest(string bucketName)
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

        public string BucketName { get; private set; }

        public string Delimiter { get; set; }

        public string Marker { get; set; }

        public int? MaxKeys { get; set; }

        public string Prefix { get; set; }
    }
}

