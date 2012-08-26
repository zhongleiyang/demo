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
    using System.IO;

    internal class PutObjectCommand : OssCommand<PutObjectResult>
    {
        private OssObject _ossObject;

        private PutObjectCommand(IServiceClient client, Uri endpoint, ExecutionContext context, IDeserializer<ServiceResponse, PutObjectResult> deserializer, OssObject ossObject) : base(client, endpoint, context, deserializer)
        {
            this._ossObject = ossObject;
        }

        public static PutObjectCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName, string key, Stream content, ObjectMetadata metadata)
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
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            if (metadata == null)
            {
                throw new ArgumentNullException("metadata");
            }
            return new PutObjectCommand(client, endpoint, context, DeserializerFactory.GetFactory().CreatePutObjectReusltDeserializer(), new OssObject(key) { BucketName = bucketName, Content = content, Metadata = metadata });
        }

        protected override Stream Content
        {
            get
            {
                return this._ossObject.Content;
            }
        }

        protected override IDictionary<string, string> Headers
        {
            get
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                this._ossObject.Metadata.Populate(headers);
                return headers;
            }
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
                return OssUtils.MakeResourcePath(this._ossObject.BucketName, this._ossObject.Key);
            }
        }
    }
}

