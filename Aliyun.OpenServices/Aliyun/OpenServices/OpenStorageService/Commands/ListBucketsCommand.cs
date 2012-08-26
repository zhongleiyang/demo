namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService.Transform;
    using System;
    using Aliyun.OpenServices.OpenStorageService.Model;
    using System.Collections.Generic;

    internal class ListBucketsCommand : OssCommand<IEnumerable<Bucket>>
    {
        public ListBucketsCommand(IServiceClient client, Uri endpoint, ExecutionContext context, IDeserializer<ServiceResponse, IEnumerable<Bucket>> deserializeMethod) : base(client, endpoint, context, deserializeMethod)
        {
        }

        public static ListBucketsCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context)
        {
            return new ListBucketsCommand(client, endpoint, context, DeserializerFactory.GetFactory().CreateListBucketResultDeserializer());
        }

        protected override string ResourcePath
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

