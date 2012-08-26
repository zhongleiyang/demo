namespace Aliyun.OpenServices.OpenStorageService.Utilities
{
    using System;

    internal class OssHeaders
    {
        public const string GetObjectIfMatch = "If-Match";
        public const string GetObjectIfModifiedSince = "If-Modified-Since";
        public const string GetObjectIfNoneMatch = "If-None-Match";
        public const string GetObjectIfUnmodifiedSince = "If-Unmodified-Since";
        public const string OssCannedAcl = "x-oss-acl";
        public const string OssPrefix = "x-oss-";
        public const string OssStorageClass = "x-oss-storage-class";
        public const string OssUserMetaPrefix = "x-oss-meta-";
        public const string OssVersionId = "x-oss-version-id";
    }
}

