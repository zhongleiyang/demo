namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Utilities;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;

    internal class SetBucketAclCommand : OssCommand
    {
        private CannedAccessControlList _acl;
        private string _bucketName;

        private SetBucketAclCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName, CannedAccessControlList acl) : base(client, endpoint, context)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "bucketName");
            }
            if (!OssUtils.IsBucketNameValid(bucketName))
            {
                throw new ArgumentException(OssResources.BucketNameInvalid, "bucketName");
            }
            this._bucketName = bucketName;
            this._acl = acl;
        }

        public static SetBucketAclCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName, CannedAccessControlList acl)
        {
            return new SetBucketAclCommand(client, endpoint, context, bucketName, acl);
        }

        protected override IDictionary<string, string> Headers
        {
            get
            {
                Dictionary<string, string> temp = new Dictionary<string, string>();
                temp.Add("x-oss-acl", this._acl.GetStringValue());
                return temp;
            }
        }

        protected override HttpMethod Method
        {
            get
            {
                return HttpMethod.Put;
            }
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                Dictionary<string, string> temp = new Dictionary<string, string>();
                temp.Add("acl", null);
                return temp;
            }
        }

        protected override string ResourcePath
        {
            get
            {
                return this._bucketName;
            }
        }
    }
}

