namespace Aliyun.OpenServices.OpenStorageService
{
    using Aliyun.OpenServices.Common.Utilities;
    using System;

    public enum CannedAccessControlList
    {
        [StringValue("private")]
        Private = 0,
        [StringValue("public-read")]
        PublicRead = 1,
        [StringValue("public-read-write")]
        PublicReadWrite = 2
    }
}

