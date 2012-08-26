namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Transform;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using System;
    using System.Collections.Generic;

    internal class GetObjectCommand : OssCommand<OssObject>
    {
        private GetObjectRequest _request;

        private GetObjectCommand(IServiceClient client, Uri endpoint, ExecutionContext context, IDeserializer<ServiceResponse, OssObject> deserializer, GetObjectRequest request) : base(client, endpoint, context, deserializer)
        {
            this._request = request;
        }

        public static GetObjectCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, GetObjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            return new GetObjectCommand(client, endpoint, context, DeserializerFactory.GetFactory().CreateGetObjectResultDeserializer(request), request);
        }

        public static GetObjectCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string bucketName, string key)
        {
            return Create(client, endpoint, context, new GetObjectRequest(bucketName, key));
        }

        protected override IDictionary<string, string> Headers
        {
            get
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                this._request.Populate(headers);
                return headers;
            }
        }

        protected override bool LeaveResponseOpen
        {
            get
            {
                return true;
            }
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                IDictionary<string, string> parameters = base.Parameters;
                this._request.ResponseHeaders.Populate(parameters);
                return parameters;
            }
        }

        protected override string ResourcePath
        {
            get
            {
                return OssUtils.MakeResourcePath(this._request.BucketName, this._request.Key);
            }
        }
    }
}

