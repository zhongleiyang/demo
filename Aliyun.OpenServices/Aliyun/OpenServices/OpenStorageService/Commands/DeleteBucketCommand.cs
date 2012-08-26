namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;

    internal class DeleteBucketCommand : OssCommand
    {
        private string _bucketName;

        private DeleteBucketCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName) : base(client, endpoint, context)
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

        public static DeleteBucketCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName)
        {
            return new DeleteBucketCommand(client, endpoint, context, bucketName);
        }

        protected override HttpMethod Method
        {
            get
            {
                return HttpMethod.Delete;
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

