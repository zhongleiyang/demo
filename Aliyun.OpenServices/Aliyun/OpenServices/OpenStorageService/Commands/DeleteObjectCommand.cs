namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;

    internal class DeleteObjectCommand : OssCommand
    {
        private string _bucketName;
        private string _key;

        private DeleteObjectCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName, string key) : base(client, endpoint, context)
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
            this._bucketName = bucketName;
            this._key = key;
        }

        public static DeleteObjectCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName, string key)
        {
            return new DeleteObjectCommand(client, endpoint, context, bucketName, key);
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
                return OssUtils.MakeResourcePath(this._bucketName, this._key);
            }
        }
    }
}

