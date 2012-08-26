namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Transform;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;

    internal class GetBucketAclCommand : OssCommand<AccessControlList>
    {
        private string _bucketName;

        private GetBucketAclCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName, IDeserializer<ServiceResponse, AccessControlList> deserializer) : base(client, endpoint, context, deserializer)
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
        }

        public static GetBucketAclCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName)
        {
            return new GetBucketAclCommand(client, endpoint, context, bucketName, DeserializerFactory.GetFactory().CreateGetAclResultDeserializer());
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

