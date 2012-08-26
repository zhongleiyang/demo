namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class ListBucketResultDeserializer : ResponseDeserializer<IEnumerable<Bucket>, ListAllMyBucketsResult>
    {
        public ListBucketResultDeserializer(IDeserializer<Stream, ListAllMyBucketsResult> contentDeserializer) : base(contentDeserializer)
        {
        }

        public override IEnumerable<Bucket> Deserialize(ServiceResponse response)
        {
            ListAllMyBucketsResult model = base.ContentDeserializer.Deserialize(response.Content);
            return (from e in model.Buckets select new Bucket(e.Name) { Owner = new Owner(model.Owner.Id, model.Owner.DisplayName), CreationDate = e.CreationDate });
        }
    }
}

