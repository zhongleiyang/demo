namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using System;

    internal class PutObjectResponseDeserializer : ResponseDeserializer<PutObjectResult, PutObjectResult>
    {
        public PutObjectResponseDeserializer() : base(null)
        {
        }

        public override PutObjectResult Deserialize(ServiceResponse response)
        {
            PutObjectResult result = new PutObjectResult();
            if (response.Headers.ContainsKey("ETag"))
            {
                result.ETag = OssUtils.TrimETag(response.Headers["ETag"]);
            }
            return result;
        }
    }
}

