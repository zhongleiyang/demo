namespace Aliyun.OpenServices.OpenStorageService
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Utilities;
    using Aliyun.OpenServices.OpenStorageService.Commands;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class OssClient
    {
        private ServiceCredentials _credentials;
        private Uri _endpoint;
        private IServiceClient _serviceClient;

        public OssClient(string accessId, string accessKey) : this(OssUtils.DefaultEndpoint, accessId, accessKey)
        {
        }

        public OssClient(string endpoint, string accessId, string accessKey) : this(new Uri(endpoint), accessId, accessKey)
        {
        }

        public OssClient(Uri endpoint, string accessId, string accessKey) : this(endpoint, accessId, accessKey, new ClientConfiguration())
        {
        }

        public OssClient(Uri endpoint, string accessId, string accessKey, ClientConfiguration configuration)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }
            if (string.IsNullOrEmpty(accessId))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "accessId");
            }
            if (string.IsNullOrEmpty(accessKey))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "accessKey");
            }
            if (!endpoint.ToString().StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(OssResources.BucketNameInvalid, "endpoint");
            }
            this._endpoint = endpoint;
            this._credentials = new ServiceCredentials(accessId, accessKey);
            this._serviceClient = ServiceClientFactory.CreateServiceClient(configuration ?? new ClientConfiguration());
        }

        public Bucket CreateBucket(string bucketName)
        {
            using (CreateBucketCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Put, bucketName, null), bucketName).Execute())
            {
            }
            return new Bucket(bucketName);
        }

        private ExecutionContext CreateContext(HttpMethod method, string bucket, string key)
        {
            ExecutionContextBuilder builder = new ExecutionContextBuilder {
                Bucket = bucket,
                Key = key,
                Method = method,
                Credentials = this._credentials
            };
            builder.ResponseHandlers.Add(new ErrorResponseHandler());
            return builder.Build();
        }

        public void DeleteBucket(string bucketName)
        {
            using (DeleteBucketCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Delete, bucketName, null), bucketName).Execute())
            {
            }
        }

        public void DeleteObject(string bucketName, string key)
        {
            DeleteObjectCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Delete, bucketName, key), bucketName, key).Execute();
        }

        public AccessControlList GetBucketAcl(string bucketName)
        {
            return GetBucketAclCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Get, bucketName, null), bucketName).Execute();
        }

        public OssObject GetObject(GetObjectRequest getObjectRequest)
        {
            if (getObjectRequest == null)
            {
                throw new ArgumentNullException("getObjectRequest");
            }
            return GetObjectCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Get, getObjectRequest.BucketName, getObjectRequest.Key), getObjectRequest).Execute();
        }

        public ObjectMetadata GetObject(GetObjectRequest getObjectRequest, Stream output)
        {
            OssObject ossObject = this.GetObject(getObjectRequest);
            using (ossObject.Content)
            {
                ossObject.Content.WriteTo(output);
            }
            return ossObject.Metadata;
        }

        public OssObject GetObject(string bucketName, string key)
        {
            return this.GetObject(new GetObjectRequest(bucketName, key));
        }

        public ObjectMetadata GetObjectMetadata(string bucketName, string key)
        {
            return GetObjectMetadataCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Head, bucketName, key), bucketName, key).Execute();
        }

        private IServiceClient GetServiceClient()
        {
            return this._serviceClient;
        }

        public IEnumerable<Bucket> ListBuckets()
        {
            return ListBucketsCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Get, null, null)).Execute();
        }

        public ObjectListing ListObjects(ListObjectsRequest listObjectsRequest)
        {
            if (listObjectsRequest == null)
            {
                throw new ArgumentNullException("listObjectsRequest");
            }
            return ListObjectsCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Get, listObjectsRequest.BucketName, null), listObjectsRequest).Execute();
        }

        public ObjectListing ListObjects(string bucketName)
        {
            return this.ListObjects(bucketName, null);
        }

        public ObjectListing ListObjects(string bucketName, string prefix)
        {
            ListObjectsRequest temp = new ListObjectsRequest(bucketName) {
                Prefix = prefix
            };
            return this.ListObjects(temp);
        }

        public PutObjectResult PutObject(string bucketName, string key, Stream content, ObjectMetadata metadata)
        {
            return PutObjectCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Put, bucketName, key), bucketName, key, content, metadata).Execute();
        }

        public void SetBucketAcl(string bucketName, CannedAccessControlList acl)
        {
            SetBucketAclCommand.Create(this.GetServiceClient(), this._endpoint, this.CreateContext(HttpMethod.Put, bucketName, null), bucketName, acl).Execute();
        }
    }
}

