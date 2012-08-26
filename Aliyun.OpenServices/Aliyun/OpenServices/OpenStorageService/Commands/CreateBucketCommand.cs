namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;

    internal class CreateBucketCommand : OssCommand
    {
        private string _bucketName;

        private CreateBucketCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName) : base(client, endpoint, context)
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

        public static CreateBucketCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName)
        {
            return new CreateBucketCommand(client, endpoint, context, bucketName);
        }

        protected override HttpMethod Method
        {
            get
            {
                return HttpMethod.Put;
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

