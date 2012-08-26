namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Transform;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using System;
    using System.Collections.Generic;

    internal class ListObjectsCommand : OssCommand<ObjectListing>
    {
        private ListObjectsRequest _listObjectsRequest;

        private ListObjectsCommand(IServiceClient client, Uri endpoint, ExecutionContext context, IDeserializer<ServiceResponse, ObjectListing> deserializer, ListObjectsRequest listObjectsRequest) : base(client, endpoint, context, deserializer)
        {
            this._listObjectsRequest = listObjectsRequest;
        }

        public static ListObjectsCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, ListObjectsRequest listObjectsRequest)
        {
            return new ListObjectsCommand(client, endpoint, context, DeserializerFactory.GetFactory().CreateListObjectsResultDeserializer(), listObjectsRequest);
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return base.Parameters;
            }
        }

        protected override string ResourcePath
        {
            get
            {
                return OssUtils.MakeResourcePath(this._listObjectsRequest.BucketName, null);
            }
        }
    }
}

