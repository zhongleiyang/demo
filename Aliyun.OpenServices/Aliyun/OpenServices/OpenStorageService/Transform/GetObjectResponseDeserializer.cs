namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenStorageService;
    using System;

    internal class GetObjectResponseDeserializer : ResponseDeserializer<OssObject, OssObject>
    {
        private GetObjectRequest _getObjectRequest;

        public GetObjectResponseDeserializer(GetObjectRequest getObjectRequest) : base(null)
        {
            this._getObjectRequest = getObjectRequest;
        }

        public override OssObject Deserialize(ServiceResponse response)
        {
            return new OssObject(this._getObjectRequest.Key) { BucketName = this._getObjectRequest.BucketName, Content = response.Content, Metadata = DeserializerFactory.GetFactory().CreateGetObjectMetadataResultDeserializer().Deserialize(response) };
        }
    }
}

