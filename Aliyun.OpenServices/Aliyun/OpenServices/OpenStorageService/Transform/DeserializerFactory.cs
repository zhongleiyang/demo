namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal abstract class DeserializerFactory
    {
        protected DeserializerFactory()
        {
        }

        protected abstract IDeserializer<Stream, T> CreateContentDeserializer<T>();
        public IDeserializer<ServiceResponse, ErrorResult> CreateErrorResultDeserializer()
        {
            return new SimpleResponseDeserializer<ErrorResult>(this.CreateContentDeserializer<ErrorResult>());
        }

        public IDeserializer<ServiceResponse, AccessControlList> CreateGetAclResultDeserializer()
        {
            return new GetAclResponseParser(this.CreateContentDeserializer<AccessControlPolicy>());
        }

        public IDeserializer<ServiceResponse, ObjectMetadata> CreateGetObjectMetadataResultDeserializer()
        {
            return new GetObjectMetadataResponseDeserializer();
        }

        public IDeserializer<ServiceResponse, OssObject> CreateGetObjectResultDeserializer(GetObjectRequest request)
        {
            return new GetObjectResponseDeserializer(request);
        }

        public IDeserializer<ServiceResponse, IEnumerable<Bucket>> CreateListBucketResultDeserializer()
        {
            return new ListBucketResultDeserializer(this.CreateContentDeserializer<ListAllMyBucketsResult>());
        }

        public IDeserializer<ServiceResponse, ObjectListing> CreateListObjectsResultDeserializer()
        {
            return new ListObjectsResponseDeserializer(this.CreateContentDeserializer<ListBucketResult>());
        }

        public IDeserializer<ServiceResponse, PutObjectResult> CreatePutObjectReusltDeserializer()
        {
            return new PutObjectResponseDeserializer();
        }

        public static DeserializerFactory GetFactory()
        {
            return GetFactory(null);
        }

        public static DeserializerFactory GetFactory(string contentType)
        {
            if (contentType == null)
            {
                contentType = "text/xml";
            }
            if (contentType.Contains("xml"))
            {
                return new XmlDeserializerFactory();
            }
            return null;
        }
    }
}

